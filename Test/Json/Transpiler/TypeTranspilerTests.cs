using LLMIntegration.Json;

namespace Test.Json.Transpiler;

public class TypeTranspilerTests
{
    [Fact]
    public void ShouldTranspileSingleTypeWithPrimitiveMembers()
    {
        // Arrange
        var transpiler = new TypeScriptTranspiler();

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
        var transpiler = new TypeScriptTranspiler();
        var type = typeof(GenericTestFun<TestFun>);

        // Act
        var typeScriptTypes = transpiler.Transpile([type], []);

        // Assert
        Assert.NotEmpty(typeScriptTypes);
        Assert.Equal(2, typeScriptTypes.Count);
        
        var genericTestFun = typeScriptTypes.First(t => t.DerivedFrom == type);
        Assert.Equal("GenericTestFun", genericTestFun.TypeName);
        Assert.Contains("Metadata", genericTestFun.Members.Select(m => m.TypeName));
        Assert.Contains("Value", genericTestFun.Members.Select(m => m.TypeName));
    }

    [Fact]
    public void ShouldTranspileTypeWithEnumerableAddTheEnumerableTypeToParentAndANonEnumerableToList()
    {
        // Arrange
        var transpiler = new TypeScriptTranspiler();
        var type = typeof(ListHolder);

        // Act
        var typeScriptTypes = transpiler.Transpile([type], []);

        // Assert
        Assert.Equal(2, typeScriptTypes.Count);
        var holder = typeScriptTypes.First(t => t.DerivedFrom == type);
        var notHolder = typeScriptTypes.First(t => t.DerivedFrom != type);
        
        Assert.False(notHolder.IsEnumerable, "The list should contain the type (unless it is primitive) but never enumerable types");
        Assert.True(holder.Members.First().IsEnumerable, "The member should be enumerable");
    }
    
    [Fact]
    public void ShouldTranspileTypeWithEnumerablePrimitiveAddTheEnumerableTypeToParentButNotToList()
    {
        // Arrange
        var transpiler = new TypeScriptTranspiler();
        var type = typeof(PrimitiveListHolder);

        // Act
        var typeScriptTypes = transpiler.Transpile([type], []);

        // Assert
        Assert.Single(typeScriptTypes);
        var holder = typeScriptTypes.First(t => t.DerivedFrom == type);
        var holderList = holder.Members.First();
        Assert.Equal(TypeScriptType.Keyword.Primitive, holderList.TypeScriptKeyword); // The list member should be primitive
        Assert.True(holderList.IsEnumerable, "The list member should be enumerable");
    }

    private record TestFun(string Hello);
    
    private record GenericTestFun<T>(T Value, string Metadata);
    
    private record ListHolder(List<TestFun> Value);
    
    private record PrimitiveListHolder(List<string> Value);
}