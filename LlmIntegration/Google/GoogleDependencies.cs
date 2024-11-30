using LLMIntegration.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace LLMIntegration.Google;

public static class GoogleDependencies
{
    public static IHttpClientBuilder RegisterGoogleDependencies(
        this IServiceCollection services,
        IConfiguration configuration,
        string userAgent)
    {
        services
            .Configure<GoogleOptions>(configuration.GetSection(GoogleOptions.SectionName));
        
        return services.AddHttpClient<IGoogleClient, GoogleClient>((sp, client) =>
        {
            var googleOptions = sp.GetRequiredService<IOptions<GoogleOptions>>();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.UserAgent.ParseAdd(userAgent);
            client.BaseAddress = new Uri(googleOptions.Value.ApiEndpoint);
        });
    }
}