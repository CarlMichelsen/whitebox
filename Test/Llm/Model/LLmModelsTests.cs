using LLMIntegration;

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
    [InlineData(true, "claude-3-5-haiku-latest")]
    [InlineData(true, "grok-3-beta")]
    [InlineData(false, "random sentence")]
    [InlineData(false, "this is in fact not a model-identifier")]
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