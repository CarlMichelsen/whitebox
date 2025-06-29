namespace Test.TestData;

internal static class TestDataReader
{
    private static readonly string TestDataDirectory = Path.Combine(GetProjectRootDirectory(), "TestData/");
    
    public static string ReadTestDataFile(string filePath)
    {
        return File.ReadAllText(Path.Combine(TestDataDirectory, filePath));
    }
    
    private static string GetProjectRootDirectory()
    {
        var dir = Directory.GetCurrentDirectory();

        while (Directory.GetFiles(dir, "*.csproj").Length == 0)
        {
            dir = Directory.GetParent(dir)!.FullName;
        }

        return dir;
    }
}