using LLMIntegration.Client;
using LLMIntegration.Google;
using LLMIntegration.Google.Dto;
using LLMIntegration.Google.Dto.Response.Stream;
using LLMIntegration.Util;
using Microsoft.Extensions.DependencyInjection;
using Test.Fake;

namespace Test.Llm.Google.Integration;

public class GoogleIntegrationTests
{
    private readonly IServiceProvider serviceProvider;

    public GoogleIntegrationTests()
    {
        var collection = new ServiceCollection();
        var configuration = TestConfiguration.GetTestConfiguration();

        collection.RegisterGoogleDependencies(configuration, "WhiteBox Test")
            .AddHttpMessageHandler(() => new TestReplayHttpDelegatingHandler());
        
        this.serviceProvider = collection.BuildServiceProvider();
    }

    [Fact]
    public async Task CanPrompt()
    {
        // Arrange
        var client = this.serviceProvider.GetRequiredService<IGoogleClient>();
        var prompt = new GooglePrompt(
            Model: LlmModels.Google.Flash15Dash8B.ModelIdentifier,
            Contents: [
                new GoogleContent(
                    Role: "user",
                    Parts: [
                        new GooglePart(Text: "Hello, World!"),
                    ]),
                new GoogleContent(
                    Role: "model",
                    Parts: [
                        new GooglePart(Text: "Hello there! How can i help you?"),
                    ]),
                new GoogleContent(
                    Role: "user",
                    Parts: [
                        new GooglePart(Text: "Write a long story about jellybeans!"),
                    ]),
            ]);

        // Act
        var res = await client.Prompt(prompt);

        // Assert
        Assert.NotNull(res);
        Assert.NotNull(res.ModelVersion);
        Assert.NotNull(res.UsageMetadata);
        Assert.IsType<int>(res.UsageMetadata.CandidatesTokenCount);
        Assert.IsType<int>(res.UsageMetadata.PromptTokenCount);
        Assert.IsType<int>(res.UsageMetadata.TotalTokenCount);
        Assert.NotEmpty(res.Candidates);
    }
    
    [Fact]
    public async Task CanStreamPrompt()
    {
        // Arrange
        var client = this.serviceProvider.GetRequiredService<IGoogleClient>();
        var prompt = new GooglePrompt(
            Model: LlmModels.Google.Flash15Dash8B.ModelIdentifier,
            Contents: [
                new GoogleContent(
                    Role: "user",
                    Parts: [
                        new GooglePart(Text: "Hello, World!"),
                    ]),
                new GoogleContent(
                    Role: "model",
                    Parts: [
                        new GooglePart(Text: "Hello there! How can i help you?"),
                    ]),
                new GoogleContent(
                    Role: "user",
                    Parts: [
                        new GooglePart(Text: "Do a flip!"),
                    ]),
            ]);

        // Act
        var events = new List<GoogleStreamChunk>();
        await foreach (var streamEvent in client.StreamPrompt(prompt))
        {
            events.Add(streamEvent);
        }

        // Assert
        Assert.NotEmpty(events);
    }
}