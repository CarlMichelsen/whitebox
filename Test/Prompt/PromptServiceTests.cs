using Application.Repository;
using Application.Service;
using Database;
using Interface.Accessor;
using Interface.Repository;
using Interface.Service;
using LLMIntegration;
using LLMIntegration.Generic;
using LLMIntegration.Generic.Dto;
using LLMIntegration.Generic.Dto.Response.Stream;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Test.Fake;

namespace Test.Prompt;

public class PromptServiceTests
{
    private readonly IServiceProvider serviceProvider;
    
    public PromptServiceTests()
    {
        var collection = new ServiceCollection();
        var configuration = TestConfiguration.GetTestConfiguration();

        collection.RegisterGenericLlmClientDependencies(
            configuration,
            "WhiteBox Test",
            () => new TestReplayHttpDelegatingHandler());
        
        collection
            .AddScoped<IPromptService, PromptService>()
            .AddScoped<IUserContextAccessor, FakeUserContextAccessor>()
            .AddScoped<IChatConfigurationRepository, ChatConfigurationRepository>()
            .AddScoped<ApplicationContext>(_ => TestDatabaseContextFactory.Create(Guid.CreateVersion7().ToString()));
        
        this.serviceProvider = collection.BuildServiceProvider();
    }
    
    [Fact]
    public async Task TestPromptRegistration()
    {
        // Arrange
        var service = this.serviceProvider.GetRequiredService<IPromptService>();
        var applicationContext = this.serviceProvider.GetRequiredService<ApplicationContext>();
        var chatConfigurationRepository = this.serviceProvider.GetRequiredService<IChatConfigurationRepository>();
        
        var user = this.serviceProvider.GetRequiredService<IUserContextAccessor>()
            .GetUserContext()
            .User;
        await chatConfigurationRepository.GetOrCreateChatConfigurationAsync(user);
        
        await applicationContext.SaveChangesAsync();
        var prompt = new LlmPrompt(
            Model: LlmModels.Anthropic.Claude37Sonnet,
            Content: new LlmContent(
                SystemMessage: "Be an elitist asshole.",
                Messages: [
                    new LlmMessage(
                        Role: LlmRole.User,
                        Parts: [
                            new LlmPart(
                                Type: PartType.Text,
                                Content: "Tell me a story about the order of things"),
                        ]),
                ]),
            MaxTokens: 2048);
        
        // Act
        var promptEntity = await service.Prompt(prompt);
        await applicationContext.SaveChangesAsync();

        // Assert
        Assert.NotNull(promptEntity);
        Assert.NotNull(promptEntity.Usage);
        Assert.NotNull(promptEntity.Usage.Completion);
        Assert.NotEmpty(promptEntity.Usage.Completion);
        Assert.NotEmpty(promptEntity.PromptJson);
        
        var foundPrompt = await applicationContext.Prompt
            .Include(p => p.Usage)
            .FirstOrDefaultAsync(p => p.Id == promptEntity.Id);
        
        Assert.NotNull(foundPrompt);
        Assert.NotNull(foundPrompt.Usage);
        Assert.NotNull(foundPrompt.Usage.Completion);
        Assert.NotEmpty(foundPrompt.Usage.Completion);
        Assert.NotEmpty(foundPrompt.PromptJson);
    }
    
    [Fact]
    public async Task TestStreamPromptRegistration()
    {
        // Arrange
        var service = this.serviceProvider.GetRequiredService<IPromptService>();
        var applicationContext = this.serviceProvider.GetRequiredService<ApplicationContext>();
        var chatConfigurationRepository = this.serviceProvider.GetRequiredService<IChatConfigurationRepository>();
        
        var user = this.serviceProvider.GetRequiredService<IUserContextAccessor>()
            .GetUserContext()
            .User;
        await chatConfigurationRepository.GetOrCreateChatConfigurationAsync(user);
        
        await applicationContext.SaveChangesAsync();
        var prompt = new LlmPrompt(
            Model: LlmModels.Anthropic.Claude37Sonnet,
            Content: new LlmContent(
                SystemMessage: "Be an elitist asshole.",
                Messages: [
                    new LlmMessage(
                        Role: LlmRole.User,
                        Parts: [
                            new LlmPart(
                                Type: PartType.Text,
                                Content: "Tell me a story about the order of things"),
                        ]),
                ]),
            MaxTokens: 2048);
        
        // Act
        var eventList = new List<LlmStreamEvent>();
        var promptEntity = await service.StreamPrompt(prompt, chunk =>
        {
            eventList.Add(chunk);
            return Task.CompletedTask;
        });

        var finalEvent = eventList.Last();
        await applicationContext.SaveChangesAsync();

        // Assert
        Assert.NotNull(promptEntity);
        Assert.NotNull(promptEntity.Usage);
        Assert.NotNull(promptEntity.Usage.Completion);
        Assert.NotEmpty(promptEntity.Usage.Completion);
        Assert.NotEmpty(promptEntity.PromptJson);
        Assert.IsType<LlmStreamConclusion>(finalEvent);
        
        var foundPrompt = await applicationContext.Prompt
            .Include(p => p.Usage)
            .FirstOrDefaultAsync(p => p.Id == promptEntity.Id);
        
        Assert.NotNull(foundPrompt);
        Assert.NotNull(foundPrompt.Usage);
        Assert.NotNull(foundPrompt.Usage.Completion);
        Assert.NotEmpty(foundPrompt.Usage.Completion);
        Assert.NotEmpty(foundPrompt.PromptJson);
    }
}