using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using LLMIntegration.Client;
using LLMIntegration.OpenAi.Dto;
using LLMIntegration.OpenAi.Dto.Response;
using LLMIntegration.OpenAi.Dto.Response.Stream;
using LLMIntegration.Util;

namespace LLMIntegration.OpenAi;

// https://platform.openai.com/docs/api-reference/chat/create
public class OpenAiClient(
    HttpClient httpClient) : IOpenAiClient
{
    private const string Path = "v1/chat/completions";
    private const string Done = "[DONE]";
    
    public async Task<OpenAiResponse> Prompt(OpenAiPrompt prompt)
    {
        var jsonData = JsonSerializer.Serialize(prompt with { Stream = false });
        var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
        var res = await httpClient.PostAsync(Path, content);
        var responseObject = await res.Content.ReadFromJsonAsync<OpenAiResponse>();
        return responseObject!;
    }

    public async IAsyncEnumerable<OpenAiChunk> StreamPrompt(OpenAiPrompt prompt)
    {
        var jsonData = JsonSerializer.Serialize(prompt with { Stream = true, StreamOptions = new OpenAiStreamOptions(true) });
        var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
        var request = new HttpRequestMessage(HttpMethod.Post, Path)
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
            if (streamLineData == Done)
            {
                yield break;
            }
            
            yield return JsonSerializer.Deserialize<OpenAiChunk>(streamLineData)!;
        }
    }
}