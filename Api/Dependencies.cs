using Api.Middleware;
using Application.Accessor;
using Application.Configuration;
using Application.Configuration.Options;
using Application.Handler;
using Application.Repository;
using Application.Service;
using Database;
using Interface.Accessor;
using Interface.Handler;
using Interface.Repository;
using Interface.Service;
using LLMIntegration.Generic;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
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
        
        // Cache
        builder.Services
            .AddMemoryCache();
        
        // Repository
        builder.Services
            .AddScoped<IConversationMessageUpsertRepository, ConversationMessageUpsertRepository>()
            .AddScoped<IChatConfigurationRepository, ChatConfigurationRepository>();
        
        // Service
        builder.Services
            .AddScoped<IChatConfigurationService, ChatConfigurationService>()
            .AddScoped<IConversationResponseStreamService, ConversationResponseStreamService>()
            .AddScoped<IPromptService, PromptService>();
        
        // Handler
        builder.Services
            .AddScoped<IConversationHandler, ConversationHandler>()
            .AddScoped<IConversationAppendHandler, ConversationAppendHandler>()
            .AddScoped<IChatConfigurationHandler, ChatConfigurationHandler>()
            .AddScoped<IModelHandler, ModelHandler>()
            .AddScoped<ISpeechToTextHandler, SpeechToTextHandler>();
        
        // Large language model integrations
        builder.Services
            .RegisterGenericLlmClientDependencies(
                builder.Configuration, 
                ApplicationConstants.ApplicationUserAgent);
        
        // Auth
        builder.RegisterAuthDependencies();
        
        // Configure Serilog from "appsettings.(env).json
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            .Enrich.WithProperty("Application", ApplicationConstants.ApplicationName)
            .Enrich.WithProperty("Environment", GetEnvironmentName(builder))
            .CreateLogger();
        builder.Host.UseSerilog();
        
        // Database
        builder.Services.AddDbContext<ApplicationContext>(options =>
        {
            options.UseNpgsql(
                builder.Configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly("Api"));
            
            if (builder.Environment.IsDevelopment())
            {
                options.EnableSensitiveDataLogging();
            }
        });
        
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