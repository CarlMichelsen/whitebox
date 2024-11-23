using Interface.Dto.Llm.Anthropic;
using LLMIntegration.Anthropic;
using Microsoft.Extensions.DependencyInjection;

namespace Test.Llm.Anthropic.Integration;

public class ClaudeIntegrationTests
{
    private readonly IServiceProvider serviceProvider;

    public ClaudeIntegrationTests()
    {
        var collection = new ServiceCollection();
        var configuration = TestConfiguration.GetTestConfiguration();

        collection.RegisterClaudeDependencies(configuration, "WhiteBox Test");
        
        this.serviceProvider = collection.BuildServiceProvider();
    }
    
    [Fact]
    public async Task CanPrompt()
    {
        // Arrange
        var client = this.serviceProvider.GetRequiredService<AnthropicClient>();
        var prompt = new AnthropicPrompt(
            Model: "claude-3-5-sonnet-20241022",
            MaxTokens: 1024,
            System: "This is a test.",
            Messages: [
                new AnthropicMessage(
                    Role: "user",
                    Content: [
                        new AnthropicContent(
                            Type: "text",
                            Text: "Please response with a short and concise verification that you're functional."),
                    ])
            ]);

        // Act
        var response = await client.Prompt(prompt);

        // Assert
        Assert.NotNull(response);
    }
}

/* This is what an error looks like:
{"type":"error","error":{"type":"invalid_request_error","message":"messages: Unexpected role \"system\". The Messages API accepts a top-level `system` parameter, not \"system\" as an input message role."}}
*/

/* This is what a successful response looks like:
{"id":"msg_01Gtmt6UiV7UyK5Goy65t7Md","type":"message","role":"assistant","model":"claude-3-5-sonnet-20241022","content":[{"type":"text","text":"I'm functioning normally and ready to help."}],"stop_reason":"end_turn","stop_sequence":null,"usage":{"input_tokens":26,"output_tokens":12}}
*/