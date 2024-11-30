using LLMIntegration.Util;

namespace Test.Llm.Model;

public class LLmModelsTests
{
    [Fact]
    public void CanListModels()
    {
        // Arrange
        // Act
        var models = LlmModels.GetModels();
        
        // Assert
        Assert.NotEmpty(models);
        Assert.Contains(LlmModels.OpenAi.Gpt4O, models);
    }
    
    [Theory]
    [InlineData(true, "claude-3-5-sonnet-latest")]
    [InlineData(true, "gemini-1.5-flash-8b")]
    [InlineData(true, "grok-beta")]
    [InlineData(false, "random sentence")]
    [InlineData(false, "this is in fact not a modelidentifier")]
    public void CanFindModelByIdentifier(bool expectedFound, string identifier)
    {
        // Arrange
        // Act
        var found = LlmModels.TryGetModel(identifier, out var model);
        
        // Assert
        Assert.Equal(expectedFound, found);
        if (expectedFound)
        {
            Assert.Equal(identifier, model!.ModelIdentifier);
        }
        else
        {
            Assert.Null(model);
        }
    }
}