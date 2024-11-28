using System.Reflection;
using Interface.Llm;
using Interface.Llm.Client;
using Interface.Llm.Dto.Anthropic;
using Interface.Llm.Dto.Anthropic.Response;
using LLMIntegration.Anthropic;
using Microsoft.Extensions.DependencyInjection;
using Test.Fake;

namespace Test.Llm.Anthropic.Integration;

public class AnthropicIntegrationTests
{
    private readonly IServiceProvider serviceProvider;

    public AnthropicIntegrationTests()
    {
        var collection = new ServiceCollection();
        var configuration = TestConfiguration.GetTestConfiguration();

        collection.RegisterAnthropicDependencies(configuration, "WhiteBox Test")
            .AddHttpMessageHandler(() => new TestReplayHttpDelegatingHandler());
        
        this.serviceProvider = collection.BuildServiceProvider();
    }
    
    [Fact]
    public async Task CanPrompt()
    {
        // Arrange
        var client = this.serviceProvider.GetRequiredService<IAnthropicClient>();
        var prompt = new AnthropicPrompt(
            Model: LlmModels.Anthropic.Claude35Haiku.ModelIdentifier,
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
        var client = this.serviceProvider.GetRequiredService<IAnthropicClient>();
        var prompt = new AnthropicPrompt(
            Model: LlmModels.Anthropic.Claude35Haiku.ModelIdentifier,
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