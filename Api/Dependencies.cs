using Api.Middleware;
using Application.Accessor;
using Application.Configuration;
using Application.Configuration.Options;
using Application.Handler;
using Interface.Accessor;
using Interface.Handler;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Serilog;

namespace Api;

public static class Dependencies
{
    public static void AddDependencies(this WebApplicationBuilder builder)
    {
        // Configuration
        builder.Configuration.AddJsonFile(
            "secrets.json",
            optional: builder.Environment.IsDevelopment(),
            reloadOnChange: false);
        
        // Middleware
        builder.Services
            .AddHttpContextAccessor()
            .AddScoped<UnhandledExceptionMiddleware>();
        
        // Accessor
        builder.Services
            .AddScoped<IUserContextAccessor, UserContextAccessor>();
        
        // Handler
        builder.Services
            .AddScoped<IConversationHandler, ConversationHandler>()
            .AddScoped<ISpeechToTextHandler, SpeechToTextHandler>();
        
        // Auth
        builder.RegisterAuthDependencies();
        
        // Configure Serilog from "appsettings.(env).json
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            .Enrich.WithProperty("Application", ApplicationConstants.ApplicationName)
            .Enrich.WithProperty("Environment", GetEnvironmentName(builder))
            .CreateLogger();
        builder.Host.UseSerilog();
        
        if (builder.Environment.IsDevelopment())
        {
            // Swagger
            builder.Services
                .AddEndpointsApiExplorer()
                .AddSwaggerGen();

            // Development CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(
                    ApplicationConstants.DevelopmentCorsPolicyName,
                    configurePolicy =>
                    {
                        configurePolicy
                            .WithOrigins(ApplicationConstants.DevelopmentCorsUrl)
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .AllowCredentials();
                    });
            });
        }
    }
    
    private static void RegisterAuthDependencies(this WebApplicationBuilder builder)
    {
        var jwtOptions = builder.Configuration
            .GetSection(JwtOptions.SectionName)
            .Get<JwtOptions>() ?? throw new NullReferenceException("Failed to get JwtOptions during startup");
        
        builder.Services.AddAuthentication().AddJwtBearer("CookieScheme", options =>
        {
            // Configure JWT settings
            options.TokenValidationParameters = TokenValidationParametersFactory
                .AccessValidationParameters(jwtOptions);

            // Get token from cookie
            options.Events = new JwtBearerEvents
            {
                OnMessageReceived = context =>
                {
                    context.Token = context.Request.Cookies[ApplicationConstants.AccessCookieName];
                    return Task.CompletedTask;
                },
            };
        });
        
        builder.Services.AddAuthorization();
    }

    private static string GetEnvironmentName(WebApplicationBuilder builder) =>
        builder.Environment.IsProduction() ? "Production" : "Development";
}