using LLMIntegration.Generic;
using LLMIntegration.Generic.Dto;
using LLMIntegration.Generic.Dto.Response.Stream;
using LLMIntegration.Util;
using Microsoft.Extensions.DependencyInjection;
using Test.Fake;

namespace Test.Llm.Generic.Integration;

public class GenericIntegrationTests
{
    private readonly IServiceProvider serviceProvider;

    public GenericIntegrationTests()
    {
        var collection = new ServiceCollection();
        var configuration = TestConfiguration.GetTestConfiguration();

        collection.RegisterGenericLlmClientDependencies(
            configuration, 
            "WhiteBox Test",
            () => new TestReplayHttpDelegatingHandler());
        
        this.serviceProvider = collection.BuildServiceProvider();
    }
    
    public static IEnumerable<object[]> GetLlmModels()
    {
        yield return [LlmModels.Anthropic.Claude35Haiku];
        yield return [LlmModels.OpenAi.Gpt4O];
        yield return [LlmModels.X.GrokBeta];
        yield return [LlmModels.Google.Flash15Dash8B];
    }

    [Theory]
    [MemberData(nameof(GetLlmModels), MemberType = typeof(GenericIntegrationTests))]
    public async Task CanPrompt(LlmModel llmModel)
    {
        // Arrange
        var client = this.serviceProvider.GetRequiredService<GenericLlmClient>();
        var prompt = new LlmPrompt(
            Model: llmModel,
            Content: new LlmContent(
                SystemMessage: "Uwu respond with anime noises.",
                Messages: [
                    new LlmMessage(
                        Role: LlmRole.User,
                        Parts: [
                            new LlmPart(
                                Type: PartType.Text,
                                Content: "Hello there!"),
                        ]),
                    new LlmMessage(
                        Role: LlmRole.Assistant,
                        Parts: [
                            new LlmPart(
                                Type: PartType.Text,
                                Content: "Uwu hao cn i halp?"),
                        ]),
                    new LlmMessage(
                        Role: LlmRole.User,
                        Parts: [
                            new LlmPart(
                                Type: PartType.Text,
                                Content: "Write a Naruto poem for me please"),
                        ]),
                ]),
            MaxTokens: 1024);

        // Act
        var response = await client.Prompt(prompt);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(LlmRole.Assistant, response.Role);
    }
    
    [Theory]
    [MemberData(nameof(GetLlmModels), MemberType = typeof(GenericIntegrationTests))]
    public async Task CanStreamPrompt(LlmModel llmModel)
    {
        // Arrange
        var client = this.serviceProvider.GetRequiredService<GenericLlmClient>();
        var prompt = new LlmPrompt(
            Model: llmModel,
            Content: new LlmContent(
                SystemMessage: "Be overly elitist in your rhetoric!",
                Messages: [
                    new LlmMessage(
                        Role: LlmRole.User,
                        Parts: [
                            new LlmPart(
                                Type: PartType.Text,
                                Content: "Hello there!"),
                        ]),
                ]),
            MaxTokens: 1024);

        // Act
        var events = new List<LlmStreamEvent>();
        await foreach (var streamEvent in client.StreamPrompt(prompt))
        {
            events.Add(streamEvent);
        }
        
        // Assert
        var foundError = events.FirstOrDefault(se => se is LlmStreamError);
        
        Assert.NotEmpty(events);
        Assert.Null(foundError);
    }
}