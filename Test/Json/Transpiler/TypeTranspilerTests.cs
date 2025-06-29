using LLMIntegration.Json;
using Xunit.Abstractions;

namespace Test.Json.Transpiler;

public class TypeTranspilerTests(
    ITestOutputHelper testOutputHelper)
{
    [Fact]
    public void ShouldTranspileSingleTypeWithPrimitiveMembers()
    {
        // Arrange
        var transpiler = new SimpleTypeScriptTranspiler();

        // Act
        var typeScriptTypes = transpiler.Transpile([typeof(TestFun)], []);

        // Assert
        Assert.NotEmpty(typeScriptTypes);
        Assert.Single(typeScriptTypes);
        Assert.Equal(nameof(TestFun), typeScriptTypes.First().TypeName);
    }
    
    [Fact]
    public void ShouldTranspileTypeWithGenerics()
    {
        // Arrange
        var transpiler = new SimpleTypeScriptTranspiler();
        var type = typeof(GenericTestFun<TestFun>);

        // Act
        var typeScriptTypes = transpiler.Transpile([type], []);
        testOutputHelper.WriteLine(string.Join("\n", typeScriptTypes));

        // Assert
        Assert.NotEmpty(typeScriptTypes);
        Assert.Equal(2, typeScriptTypes.Count);
        
        var genericTestFun = typeScriptTypes.First(t => t.DerivedFrom == type);
        Assert.Equal("GenericTestFun", genericTestFun.TypeName);
        Assert.Contains("Metadata", genericTestFun.Members.Select(m => m.MemberName));
        Assert.Contains("string", genericTestFun.Members.Select(m => m.Type.TypeName));
        
        Assert.Contains("Value", genericTestFun.Members.Select(m => m.MemberName));
        Assert.Contains("TestFun", genericTestFun.Members.Select(m => m.Type.TypeName));
        
        Assert.Empty(genericTestFun.Generics); // There should not be a defined generic on this type because it is explicit
    }

    [Fact]
    public void ShouldTranspileTypeWithEnumerableAddTheEnumerableTypeToParentAndANonEnumerableToList()
    {
        // Arrange
        var transpiler = new SimpleTypeScriptTranspiler();
        var type = typeof(ListHolder);

        // Act
        var typeScriptTypes = transpiler.Transpile([type], []);

        // Assert
        Assert.Equal(2, typeScriptTypes.Count);
        var holder = typeScriptTypes.First(t => t.DerivedFrom == type);
        var notHolder = typeScriptTypes.First(t => t.DerivedFrom != type);
        
        Assert.False(notHolder.IsEnumerable, "The list should contain the type (unless it is primitive) but never enumerable types");
        var holderList = holder.Members.First();
        Assert.True(holderList.Type.IsEnumerable, "The member should be enumerable");
        Assert.Equal(TypeScriptType.Keyword.Type, holderList.Type.TypeScriptKeyword);
    }
    
    [Fact]
    public void ShouldTranspileTypeWithEnumerablePrimitiveAddTheEnumerableTypeToParentButNotToList()
    {
        // Arrange
        var transpiler = new SimpleTypeScriptTranspiler();
        var type = typeof(PrimitiveListHolder);

        // Act
        var typeScriptTypes = transpiler.Transpile([type], []);

        // Assert
        Assert.Single(typeScriptTypes);
        var holder = typeScriptTypes.First(t => t.DerivedFrom == type);
        var holderList = holder.Members.First();
        Assert.Equal(TypeScriptType.Keyword.Primitive, holderList.Type.TypeScriptKeyword); // The list member should be primitive
        Assert.True(holderList.Type.IsEnumerable, "The list member should be enumerable");
        Assert.Equal(TypeScriptType.Keyword.Primitive, holderList.Type.TypeScriptKeyword);
    }

    private record TestFun(string Hello);
    
    private record GenericTestFun<T>(T Value, string Metadata);
    
    private record ListHolder(List<TestFun> Value);
    
    private record PrimitiveListHolder(List<string> Value);
}