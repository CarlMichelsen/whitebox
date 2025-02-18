using System.IO.Compression;
using Api.Logging;
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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Api;

public static class Dependencies
{
    public static void AddApplicationDependencies(this WebApplicationBuilder builder)
    {
        // Configuration
        builder.Services
            .ConfigureApplicationOpenApi();
        
        builder.Configuration.AddJsonFile(
            "secrets.json",
            optional: builder.Environment.IsDevelopment(),
            reloadOnChange: false);
        
        // Middleware
        builder.Services
            .AddHttpContextAccessor()
            .AddScoped<TraceIdMiddleware>()
            .AddProblemDetails(options =>
            {
                options.CustomizeProblemDetails = context =>
                {
                    // Let problem-details know what the trace-identifier is.
                    context.ProblemDetails.Extensions["traceId"] = 
                        context.HttpContext.TraceIdentifier;
                };
            })
            .AddExceptionHandler<GlobalExceptionHandler>();
        
        // Accessor
        builder.Services
            .AddScoped<ISourceIdAccessor, SourceIdAccessor>()
            .AddScoped<IUserContextAccessor, UserContextAccessor>();
        
        // Cache
        builder.Services
            .AddMemoryCache();
        
        // Repository
        builder.Services
            .AddScoped<IFullConversationReaderRepository, FullConversationReaderRepository>()
            .AddScoped<IConversationMessageUpsertRepository, ConversationMessageUpsertRepository>()
            .AddScoped<IChatConfigurationRepository, ChatConfigurationRepository>();
        
        // Service
        builder.Services
            .AddScoped<IChatConfigurationService, ChatConfigurationService>()
            .AddScoped<ICacheService, CacheService>()
            .AddScoped<IConversationService, ConversationService>()
            .AddScoped<IConversationStreamService, ConversationStreamService>()
            .AddScoped<IPromptService, PromptService>()
            .AddScoped<IRedirectRegistrationService, RedirectRegistrationService>();
        
        // Handler
        builder.Services
            .AddScoped<IConversationHandler, ConversationHandler>()
            .AddScoped<IConversationStreamHandler, ConversationStreamHandler>()
            .AddScoped<IChatConfigurationHandler, ChatConfigurationHandler>()
            .AddScoped<IModelHandler, ModelHandler>()
            .AddScoped<ISpeechToTextHandler, SpeechToTextHandler>();
        
        // Large language model integrations
        builder.Services
            .RegisterGenericLlmClientDependencies(
                builder.Configuration, 
                ApplicationConstants.UserAgent);
        
        // Auth
        builder.RegisterAuthDependencies();
        
        // Compression
        builder.Services.Configure<GzipCompressionProviderOptions>(options =>
        {
            options.Level = CompressionLevel.SmallestSize;
        });
        builder.Services.AddResponseCompression(options =>
        {
            options.EnableForHttps = true;
            options.Providers.Add<BrotliCompressionProvider>();
            options.Providers.Add<GzipCompressionProvider>();
        });
        
        // Serilog
        builder.Host.UseSerilog((context, sp, configuration) =>
        {
            configuration
                .ReadFrom.Configuration(context.Configuration)
                .ReadFrom.Services(sp)
                .Enrich.With(sp.GetRequiredService<TraceIdEnricher>())
                .Enrich.WithProperty("Application", ApplicationConstants.Name)
                .Enrich.WithProperty("Environment", GetEnvironmentName(builder));
        });
        builder.Services.AddSingleton<TraceIdEnricher>();
        
        // Database
        builder.Services.AddDbContext<ApplicationContext>(options =>
        {
            options.UseNpgsql(
                builder.Configuration.GetConnectionString("DefaultConnection"),
                b =>
                {
                    b.MigrationsAssembly("Api");
                    b.MigrationsHistoryTable("__EFMigrationsHistory", ApplicationContext.SchemaName);
                })
                .UseSnakeCaseNamingConvention();
            
            if (builder.Environment.IsDevelopment())
            {
                options.EnableSensitiveDataLogging();
            }
        });
        
        if (builder.Environment.IsDevelopment())
        {
            // Swagger
            builder.Services
                .AddEndpointsApiExplorer();

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