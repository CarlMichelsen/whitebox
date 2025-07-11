﻿using System.Net.Http.Headers;
using LLMIntegration.Interface;
using LLMIntegration.Util;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace LLMIntegration.X;

public static class XAiDependencies
{
    public static IHttpClientBuilder RegisterXAiDependencies(
        this IServiceCollection services,
        IConfiguration configuration,
        string userAgent)
    {
        services
            .Configure<XAiOptions>(configuration.GetSection(XAiOptions.SectionName));
        
        return services.AddHttpClient<IXAiClient, XAiClient>((sp, client) =>
        {
            client.Timeout = TimeSpan.FromMinutes(15);
            var xAiOptions = sp.GetRequiredService<IOptions<XAiOptions>>();
            var apiKey = ApiKeyUtil.GetRandomKey(xAiOptions.Value.ApiKeys);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Bearer",
                apiKey);
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.UserAgent.ParseAdd(userAgent);
            client.BaseAddress = new Uri(xAiOptions.Value.ApiEndpoint);
        });
    }
}