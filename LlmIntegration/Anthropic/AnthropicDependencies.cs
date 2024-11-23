using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace LLMIntegration.Anthropic;

public static class AnthropicDependencies
{
    public static ServiceCollection RegisterClaudeDependencies(this ServiceCollection services, IConfigurationRoot configuration, string userAgent)
    {
        services
            .Configure<AnthropicOptions>(configuration.GetSection(AnthropicOptions.SectionName));
        
        services.AddHttpClient<AnthropicClient>((sp, client) =>
        {
            var claudeOptions = sp.GetRequiredService<IOptions<AnthropicOptions>>();
            client.DefaultRequestHeaders.Add("x-api-key", claudeOptions.Value.ApiKeys.First());
            client.DefaultRequestHeaders.Add("anthropic-version", claudeOptions.Value.AnthropicVersion);
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.UserAgent.ParseAdd(userAgent);
            client.BaseAddress = new Uri(claudeOptions.Value.ApiEndPoint);
        });

        return services;
    }
}