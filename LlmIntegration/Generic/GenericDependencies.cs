using LLMIntegration.Anthropic;
using LLMIntegration.Google;
using LLMIntegration.OpenAi;
using LLMIntegration.X;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LLMIntegration.Generic;

public static class GenericDependencies
{
    public static ServiceCollection RegisterGenericLlmClientDependencies(
        this ServiceCollection services,
        IConfigurationRoot configuration,
        string userAgent)
    {
        services.RegisterAnthropicDependencies(configuration, userAgent);
        services.RegisterGoogleDependencies(configuration, userAgent);
        services.RegisterOpenAiDependencies(configuration, userAgent);
        services.RegisterXAiDependencies(configuration, userAgent);
        services.AddScoped<GenericLlmClient>();

        return services;
    }
}