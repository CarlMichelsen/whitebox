using System.Text.Json;
using Interface.Dto.Llm.Google;
using LLMIntegration.Google;
using Microsoft.Extensions.DependencyInjection;

namespace Test.Llm.Google.Integration;

public class GoogleIntegrationTests
{
    private readonly IServiceProvider serviceProvider;

    public GoogleIntegrationTests()
    {
        var collection = new ServiceCollection();
        var configuration = TestConfiguration.GetTestConfiguration();

        collection.RegisterGoogleDependencies(configuration, "WhiteBox Test");
        
        this.serviceProvider = collection.BuildServiceProvider();
    }

    [Fact]
    public async Task CanPrompt()
    {
        // Arrange
        var client = this.serviceProvider.GetRequiredService<GoogleClient>();
        var prompt = new GooglePrompt(
            Model: "gemini-1.5-flash",
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
        var client = this.serviceProvider.GetRequiredService<GoogleClient>();
        var prompt = new GooglePrompt(
            Model: "gemini-1.5-flash",
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
        var events = new List<object>();
        await foreach (var streamEvent in client.StreamPrompt(prompt))
        {
            Console.WriteLine(JsonSerializer.Serialize(streamEvent));
            events.Add(streamEvent);
        }

        // Assert
    }
}