using Interface.Llm;
using Interface.Llm.Dto.Generic;
using LLMIntegration.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace Test.Llm.Generic.Integration;

public class GenericIntegrationTests
{
    private readonly IServiceProvider serviceProvider;

    public GenericIntegrationTests()
    {
        var collection = new ServiceCollection();
        var configuration = TestConfiguration.GetTestConfiguration();

        collection.RegisterGenericLlmClientDependencies(configuration, "WhiteBox Test");
        
        this.serviceProvider = collection.BuildServiceProvider();
    }
    
    [Fact]
    public async Task CanPrompt()
    {
        // Arrange
        var client = this.serviceProvider.GetRequiredService<GenericLlmClient>();
        var prompt = new LlmPrompt(
            Model: LlmModels.Anthropic.Claude35Haiku,
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
    }
}