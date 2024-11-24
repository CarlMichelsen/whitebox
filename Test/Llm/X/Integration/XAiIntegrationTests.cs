﻿using System.Text.Json;
using Interface.Dto.Llm.OpenAi;
using Interface.Dto.Llm.OpenAi.Response.Stream;
using LLMIntegration.OpenAi;
using LLMIntegration.X;
using Microsoft.Extensions.DependencyInjection;

namespace Test.Llm.OpenAi.Integration;

public class XAiIntegrationTests
{
    private readonly IServiceProvider serviceProvider;

    public XAiIntegrationTests()
    {
        var collection = new ServiceCollection();
        var configuration = TestConfiguration.GetTestConfiguration();

        collection.RegisterXAiDependencies(configuration, "WhiteBox Test");
        
        this.serviceProvider = collection.BuildServiceProvider();
    }
    
    [Fact]
    public async Task CanPrompt()
    {
        // Arrange
        var client = this.serviceProvider.GetRequiredService<XAiClient>();
        var prompt = new OpenAiPrompt(
            Model: "grok-beta",
            Messages: [
                new OpenAiMessage(
                    Role: "system",
                    Content: [
                        new OpenAiContent(Type: "text", Text: "Be a nice little bot uwu")
                    ]),
                new OpenAiMessage(
                    Role: "user",
                    Content: [
                        new OpenAiContent(Type: "text", Text: "Hello there robot, tell be about yourself!")
                    ]),
            ],
            MaxTokens: 1024,
            ResponseFormat: null,
            Stream: false,
            AllowOpenAiToStoreConversation: false);

        // Act
        var response = await client.Prompt(prompt);

        // Assert
        Assert.NotNull(response);
        Assert.NotEmpty(response.Choices);
        Assert.Equal("assistant", response.Choices.First().Message.Role);
        Assert.False(string.IsNullOrWhiteSpace(response.Choices.First().Message.Content));
    }

    [Fact]
    public async Task CanStreamPrompt()
    {
        // Arrange
        var client = this.serviceProvider.GetRequiredService<XAiClient>();
        var prompt = new OpenAiPrompt(
            Model: "grok-beta",
            Messages: [
                new OpenAiMessage(
                    Role: "system",
                    Content: [
                        new OpenAiContent(Type: "text", Text: "Be a nice little bot uwu")
                    ]),
                new OpenAiMessage(
                    Role: "user",
                    Content: [
                        new OpenAiContent(Type: "text", Text: "Hello there robot, tell be about yourself!")
                    ]),
            ],
            MaxTokens: 1024,
            ResponseFormat: null,
            Stream: false,
            AllowOpenAiToStoreConversation: false);
        
        // Act
        var events = new List<OpenAiChunk>();
        await foreach (var streamEvent in client.StreamPrompt(prompt))
        {
            events.Add(streamEvent);
        }
        
        // Assert
        Assert.NotEmpty(events);
    }
}