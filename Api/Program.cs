using Api;
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

app.UseAuthentication();

app.UseAuthorization();

app.UseStaticFiles(StaticFileOptionsFactory.Create());

app.RegisterEndpoints();

app.MapGet("health", () => Results.Ok());

app.MapFallbackToFile("index.html");

app.Services.GetRequiredService<ILogger<Program>>()
    .LogInformation(
        "{ApplicationName} service has started",
        ApplicationConstants.ApplicationName);

app.Run();