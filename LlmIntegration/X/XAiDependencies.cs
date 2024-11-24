using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace LLMIntegration.X;

public static class XAiDependencies
{
    public static ServiceCollection RegisterXAiDependencies(this ServiceCollection services, IConfigurationRoot configuration, string userAgent)
    {
        services
            .Configure<XAiOptions>(configuration.GetSection(XAiOptions.SectionName));
        
        services.AddHttpClient<XAiClient>((sp, client) =>
        {
            var xAiOptions = sp.GetRequiredService<IOptions<XAiOptions>>();
            var apiKey = ApiKeyUtil.GetRandomKey(xAiOptions.Value.ApiKeys);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Bearer",
                apiKey);
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.UserAgent.ParseAdd(userAgent);
            client.BaseAddress = new Uri(xAiOptions.Value.ApiEndpoint);
        });

        return services;
    }
}