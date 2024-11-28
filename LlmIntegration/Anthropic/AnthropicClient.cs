using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Interface.Llm.Client;
using Interface.Llm.Dto.Anthropic;
using Interface.Llm.Dto.Anthropic.Response;
using LLMIntegration.Util;

namespace LLMIntegration.Anthropic;

// https://docs.anthropic.com/en/api/messages-streaming
public class AnthropicClient(
    HttpClient httpClient) : IAnthropicClient
{
    private const string Path = "v1/messages";
    
    public async Task<AnthropicResponse> Prompt(AnthropicPrompt prompt)
    {
        var jsonData = JsonSerializer.Serialize(prompt with { Stream = false });
        var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
        var res = await httpClient.PostAsync(Path, content);
        var responseObject = await res.Content.ReadFromJsonAsync<AnthropicResponse>();
        return responseObject!;
    }
    
    public async IAsyncEnumerable<BaseAnthropicEvent> StreamPrompt(AnthropicPrompt prompt)
    {
        var jsonData = JsonSerializer.Serialize(prompt with { Stream = true });
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
            
            var streamLineType = line[..split];
            var streamLineData = line[(split + 1)..].Trim();
            if (streamLineType == "data")
            {
                yield return JsonSerializer.Deserialize<BaseAnthropicEvent>(streamLineData)!;
            }
        }
        
        yield break;
    }
}