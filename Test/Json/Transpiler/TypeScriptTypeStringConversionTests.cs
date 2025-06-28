using LLMIntegration.Json;
using Xunit.Abstractions;

namespace Test.Json.Transpiler;

public class TypeScriptTypeStringConversionTests(
    ITestOutputHelper testOutputHelper)
{
    [Fact]
    public void TypeScriptTypeStringConversionTest()
    {
        // Arrange
        const string typeName = "TextValueObject";
        var type = new TypeScriptType(
            TypeScriptType.Keyword.Type,
            null,
            typeName,
            [
                new TypeScriptTypeMember(
                    "Value",
                    new TypeScriptType(
                        TypeScriptType.Keyword.Primitive,
                        typeof(string),
                        "string"))
                ]);

        // Act
        var typeScriptString = type.ToString();
        testOutputHelper.WriteLine(typeScriptString);

        // Assert
        Assert.Contains(typeName, typeScriptString);
        Assert.Contains("type", typeScriptString);
        Assert.Contains("{", typeScriptString);
        foreach (var member in type.Members)
        {
            Assert.Contains(member.MemberName, typeScriptString);
            Assert.Contains(member.Type.TypeName, typeScriptString);
        }
        
        Assert.Contains("}", typeScriptString);
    }
}