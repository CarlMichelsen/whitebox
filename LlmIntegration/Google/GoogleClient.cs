using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using LLMIntegration.Google.Dto;
using LLMIntegration.Google.Dto.Response;
using LLMIntegration.Google.Dto.Response.Stream;
using LLMIntegration.Interface;
using LLMIntegration.Util;
using Microsoft.Extensions.Options;

namespace LLMIntegration.Google;

// https://ai.google.dev/gemini-api/docs/text-generation?lang=rest
public class GoogleClient(
    HttpClient httpClient,
    IOptions<GoogleOptions> googleOptions) : IGoogleClient
{
    public async Task<GoogleResponse> Prompt(GooglePrompt prompt)
    {
        var jsonData = JsonSerializer.Serialize(prompt);
        var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
        var apiKey = ApiKeyUtil.GetRandomKey(googleOptions.Value.ApiKeys);
        var res = await httpClient.PostAsync(GeneratePath(prompt.Model, apiKey, false), content);
        var responseObject = await res.Content.ReadFromJsonAsync<GoogleResponse>();
        return responseObject!;
    }

    public async IAsyncEnumerable<GoogleStreamChunk> StreamPrompt(GooglePrompt prompt)
    {
        var jsonData = JsonSerializer.Serialize(prompt);
        var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
        var apiKey = ApiKeyUtil.GetRandomKey(googleOptions.Value.ApiKeys);
        var request = new HttpRequestMessage(HttpMethod.Post, GeneratePath(prompt.Model, apiKey, true))
        {
            Content = content,
        };
        httpClient.DefaultRequestHeaders.Add(LlmConstants.LlmIsStreamHeaderName, "true");
        var res = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
        
        await using var responseStream = await res.Content.ReadAsStreamAsync();
        using var streamReader = new StreamReader(responseStream);

        while (!streamReader.EndOfStream)
        {
            var line = await streamReader.ReadLineAsync();
            if (line == null)
            {
                continue;
            }
            
            var split = line.IndexOf(':');
            if (split == -1)
            {
                continue;
            }
            
            var streamLineData = line[(split + 1)..].Trim();
            yield return JsonSerializer.Deserialize<GoogleStreamChunk>(streamLineData)!;
        }
    }

    private static string GeneratePath(string model, string apiKey, bool stream)
    {
        return $"/v1beta/models/{model}:{(stream ? "streamGenerateContent?alt=sse&" : "generateContent?")}key={apiKey}";
    }
}

/*
data: {"candidates": [{"content": {"parts": [{"text": "I"}],"role": "model"}}],"usageMetadata": {"promptTokenCount": 20,"totalTokenCount": 20},"modelVersion": "gemini-1.5-flash-002"}
data: {"candidates": [{"content": {"parts": [{"text": " can't physically do a flip.  I'm a large language model"}],"role": "model"}}],"usageMetadata": {"promptTokenCount": 20,"totalTokenCount": 20},"modelVersion": "gemini-1.5-flash-002"}
data: {"candidates": [{"content": {"parts": [{"text": ", existing only as code and data on a computer.  But I can imagine"}],"role": "model"}}],"usageMetadata": {"promptTokenCount": 20,"totalTokenCount": 20},"modelVersion": "gemini-1.5-flash-002"}
data: {"candidates": [{"content": {"parts": [{"text": " doing a flip!  ??\u200d
?\n"}],"role": "model"},"finishReason": "STOP"}],"usageMetadata": {"promptTokenCount": 20,"candidatesTokenCount": 41,"totalTokenCount": 61},"modelVersion": "gemini-1.5-flash-002"}
*/