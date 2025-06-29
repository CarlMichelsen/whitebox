using System.Text.RegularExpressions;
using LLMIntegration.Json;
using Test.TestData;

namespace Test.Json.Transpiler;

public class TypeScriptTypeGenericsTests
{
    [Fact]
    public void TypeScriptTypeStringConversionTest()
    {
        // Arrange
        var transpiler = new SimpleTypeScriptTranspiler();

        // Act
        var typescriptTypes = transpiler.Transpile([typeof(GenericValueObject<>)], []);
        var typeScriptStringContent = string.Join("\n\n", typescriptTypes);

        // Assert
        var testContent = TestDataReader.ReadTestDataFile("TypeScript/GenericValueObject.ts");
        Assert.Equal(
            RemoveAllWhitespaceCharacters(testContent),
            RemoveAllWhitespaceCharacters(typeScriptStringContent),
            ignoreWhiteSpaceDifferences: true);
    }

    private static string RemoveAllWhitespaceCharacters(string text)
    {
        return Regex.Replace(text, @"\s+", string.Empty);
    }

    private record GenericValueObject<T>(T Value, string Metadata, List<OogaBooga<string, int>> OogaList);
    
    private record OogaBooga<T1, T2>(T1 Ooga, T2 Booga);
}