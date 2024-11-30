using LLMIntegration.Interface;
using LLMIntegration.Util;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace LLMIntegration.Anthropic;

public static class AnthropicDependencies
{
    public static IHttpClientBuilder RegisterAnthropicDependencies(
        this IServiceCollection services,
        IConfiguration configuration,
        string userAgent)
    {
        services
            .Configure<AnthropicOptions>(configuration.GetSection(AnthropicOptions.SectionName));
        
        return services.AddHttpClient<IAnthropicClient, AnthropicClient>((sp, client) =>
        {
            var anthropicOptions = sp.GetRequiredService<IOptions<AnthropicOptions>>();
            var apiKey = ApiKeyUtil.GetRandomKey(anthropicOptions.Value.ApiKeys);
            client.DefaultRequestHeaders.Add("x-api-key", apiKey);
            client.DefaultRequestHeaders.Add("anthropic-version", anthropicOptions.Value.AnthropicVersion);
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.UserAgent.ParseAdd(userAgent);
            client.BaseAddress = new Uri(anthropicOptions.Value.ApiEndpoint);
        });
    }
}