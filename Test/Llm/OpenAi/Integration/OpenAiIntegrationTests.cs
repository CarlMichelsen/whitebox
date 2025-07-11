﻿using LLMIntegration;
using LLMIntegration.Interface;
using LLMIntegration.OpenAi;
using LLMIntegration.OpenAi.Dto;
using LLMIntegration.OpenAi.Dto.Response.Stream;
using Microsoft.Extensions.DependencyInjection;
using Test.Fake;

namespace Test.Llm.OpenAi.Integration;

public class OpenAiIntegrationTests
{
    private readonly IServiceProvider serviceProvider;

    public OpenAiIntegrationTests()
    {
        var collection = new ServiceCollection();
        var configuration = TestConfiguration.GetTestConfiguration();

        collection.RegisterOpenAiDependencies(configuration, "WhiteBox Test")
            .AddHttpMessageHandler(() => new TestReplayHttpDelegatingHandler());
        
        this.serviceProvider = collection.BuildServiceProvider();
    }
    
    [Fact]
    public async Task CanPrompt()
    {
        // Arrange
        var client = this.serviceProvider.GetRequiredService<IOpenAiClient>();
        var prompt = new OpenAiPrompt(
            Model: LlmModels.OpenAi.Gpt4O.ModelIdentifier,
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
        Assert.Equal("assistant", response.Choices.First().ResponseMessage.Role);
        Assert.False(string.IsNullOrWhiteSpace(response.Choices.First().ResponseMessage.Content));
    }

    [Fact]
    public async Task CanStreamPrompt()
    {
        // Arrange
        var client = this.serviceProvider.GetRequiredService<IOpenAiClient>();
        var prompt = new OpenAiPrompt(
            Model: LlmModels.OpenAi.Gpt4O.ModelIdentifier,
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