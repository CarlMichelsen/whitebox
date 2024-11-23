using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Interface.Client;
using Interface.Dto.Llm.Anthropic;
using Interface.Dto.Llm.Anthropic.Response;

namespace LLMIntegration.Anthropic;

public class AnthropicClient(
    HttpClient httpClient) : ILlmClient<AnthropicPrompt, AnthropicResponse, BaseAnthropicEvent>
{
    public async Task<AnthropicResponse> Prompt(AnthropicPrompt prompt)
    {
        var jsonData = JsonSerializer.Serialize(prompt with { Stream = false });
        var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
        var res = await httpClient.PostAsync("v1/messages", content);
        var responseObject = await res.Content.ReadFromJsonAsync<AnthropicResponse>();
        return responseObject!;
    }
    
    public async IAsyncEnumerable<BaseAnthropicEvent> StreamPrompt(AnthropicPrompt prompt)
    {
        var jsonData = JsonSerializer.Serialize(prompt with { Stream = true });
        var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
        var res = await httpClient.PostAsync("v1/messages", content);
        
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
    }
}