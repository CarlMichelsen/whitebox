using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace LLMIntegration.Anthropic;

public static class AnthropicDependencies
{
    public static ServiceCollection RegisterAnthropicDependencies(this ServiceCollection services, IConfigurationRoot configuration, string userAgent)
    {
        services
            .Configure<AnthropicOptions>(configuration.GetSection(AnthropicOptions.SectionName));
        
        services.AddHttpClient<AnthropicClient>((sp, client) =>
        {
            var anthropicOptions = sp.GetRequiredService<IOptions<AnthropicOptions>>();
            var apiKey = ApiKeyUtil.GetRandomKey(anthropicOptions.Value.ApiKeys);
            client.DefaultRequestHeaders.Add("x-api-key", apiKey);
            client.DefaultRequestHeaders.Add("anthropic-version", anthropicOptions.Value.AnthropicVersion);
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.UserAgent.ParseAdd(userAgent);
            client.BaseAddress = new Uri(anthropicOptions.Value.ApiEndpoint);
        });

        return services;
    }
}