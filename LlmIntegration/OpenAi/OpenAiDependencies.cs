﻿using System.Net.Http.Headers;
using Interface.Llm.Client;
using LLMIntegration.Util;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace LLMIntegration.OpenAi;

public static class OpenAiDependencies
{
    public static IHttpClientBuilder RegisterOpenAiDependencies(
        this ServiceCollection services,
        IConfigurationRoot configuration,
        string userAgent)
    {
        services
            .Configure<OpenAiOptions>(configuration.GetSection(OpenAiOptions.SectionName));
        
        return services.AddHttpClient<IOpenAiClient, OpenAiClient>((sp, client) =>
        {
            var openAiOptions = sp.GetRequiredService<IOptions<OpenAiOptions>>();
            var apiKey = ApiKeyUtil.GetRandomKey(openAiOptions.Value.ApiKeys);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Bearer",
                apiKey);
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.UserAgent.ParseAdd(userAgent);
            client.BaseAddress = new Uri(openAiOptions.Value.ApiEndpoint);
        });
    }
}