using System.Reflection;
using System.Text.Json;
using Interface.Dto.Llm.Anthropic;
using Interface.Dto.Llm.Anthropic.Response;
using LLMIntegration.Anthropic;
using Microsoft.Extensions.DependencyInjection;

namespace Test.Llm.Anthropic.Integration;

public class AnthropicIntegrationTests
{
    private readonly IServiceProvider serviceProvider;

    public AnthropicIntegrationTests()
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
        Assert.NotNull(response.Content.First().Text);
    }
    
    [Fact]
    public async Task CanStreamPrompt()
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
        
        // Get all derived types of BaseEvent
        var derivedTypes = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(t => t.IsSubclassOf(typeof(BaseAnthropicEvent)));

        // Act
        var events = new List<BaseAnthropicEvent>();
        await foreach (var streamEvent in client.StreamPrompt(prompt))
        {
            events.Add(streamEvent);
        }
        
        // Assert
        foreach (var type in derivedTypes)
        {
            var containsType = events.Any(item => item.GetType() == type);
            Assert.True(containsType, $"The list does not contain any instance of type {type.Name}");
        }
    }
}