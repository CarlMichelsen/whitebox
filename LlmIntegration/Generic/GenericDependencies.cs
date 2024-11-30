using LLMIntegration.Anthropic;
using LLMIntegration.Google;
using LLMIntegration.OpenAi;
using LLMIntegration.X;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LLMIntegration.Generic;

public static class GenericDependencies
{
    public static IServiceCollection RegisterGenericLlmClientDependencies(
        this IServiceCollection services,
        IConfiguration configuration,
        string userAgent,
        Func<DelegatingHandler>? llmClientDelegatingHandler = default)
    {
        services.RegisterAnthropicDependencies(configuration, userAgent)
            .AddOptionalDelegatingHandler(llmClientDelegatingHandler);
        
        services.RegisterGoogleDependencies(configuration, userAgent)
            .AddOptionalDelegatingHandler(llmClientDelegatingHandler);
        
        services.RegisterOpenAiDependencies(configuration, userAgent)
            .AddOptionalDelegatingHandler(llmClientDelegatingHandler);
        
        services.RegisterXAiDependencies(configuration, userAgent)
            .AddOptionalDelegatingHandler(llmClientDelegatingHandler);
        
        services
            .AddScoped<GenericAnthropicClient>()
            .AddScoped<GenericGoogleClient>()
            .AddScoped<GenericOpenAiClient>()
            .AddScoped<GenericXAiClient>()
            .AddScoped<GenericLlmClient>();

        return services;
    }

    private static void AddOptionalDelegatingHandler(
        this IHttpClientBuilder builder,
        Func<DelegatingHandler>? configureHandle)
    {
        if (configureHandle is not null)
        {
            builder.AddHttpMessageHandler(configureHandle);
        }
    }
}