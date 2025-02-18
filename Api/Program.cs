using Api;
using Api.Extensions;
using Api.Middleware;
using Application.Configuration;
using Scalar.AspNetCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.AddApplicationDependencies();

var app = builder.Build();

app.UseMiddleware<TraceIdMiddleware>();

app.UseSerilogRequestLogging();

app.UseExceptionHandler(_ => { });

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi()
        .CacheOutput();

    app.MapScalarApiReference();
}
else
{
    // "Why make trillions, when we can make... billions?" - Dr. Evil
    await app.Services.EnsureDatabaseUpdated();
}

app.UseAuthentication();

app.UseAuthorization();

app.UseResponseCompression();

app.UseStaticFiles(StaticFileOptionsFactory.Create());

app.RegisterEndpoints();

app.Services.GetRequiredService<ILogger<Program>>()
    .LogInformation(
        "{ApplicationName} service has started",
        ApplicationConstants.Name);

app.Run();