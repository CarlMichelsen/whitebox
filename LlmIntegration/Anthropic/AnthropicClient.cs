using System.Text;
using System.Text.Json;
using Interface.Client;
using Interface.Dto.Llm.Anthropic;

namespace LLMIntegration.Anthropic;

public class AnthropicClient(HttpClient httpClient) : ILlmClient
{
    public async Task<string> Prompt(AnthropicPrompt prompt)
    {
        var jsonData = JsonSerializer.Serialize(prompt);
        var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
        var res = await httpClient.PostAsync("v1/messages", content);
        return await res.Content.ReadAsStringAsync();
    }
}