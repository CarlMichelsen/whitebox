using Api;
using Api.Middleware;
using Domain.Configuration;

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

app.UseAuthentication();

app.UseAuthorization();

app.UseStaticFiles(StaticFileOptionsFactory.Create());

app.MapFallbackToFile("index.html");

app.RegisterEndpoints();

app.MapGet("health", () => Results.Ok());

app.Services.GetRequiredService<ILogger<Program>>()
    .LogInformation(
        "{ApplicationName} service has started",
        ApplicationConstants.ApplicationName);

app.Run();