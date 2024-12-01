using Api;
using Api.Extensions;
using Api.Middleware;
using Application.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.AddDependencies();

var app = builder.Build();

app.UseMiddleware<UnhandledExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors(ApplicationConstants.DevelopmentCorsPolicyName);
}
else
{
    // "Why make trillions, when we can make... billions?" - Dr. Evil
    await app.Services.EnsureDatabaseUpdated();
}

app.UseAuthentication();

app.UseAuthorization();

app.UseStaticFiles(StaticFileOptionsFactory.Create());

app.RegisterEndpoints();

app.Services.GetRequiredService<ILogger<Program>>()
    .LogInformation(
        "{ApplicationName} service has started",
        ApplicationConstants.ApplicationName);

app.Run();