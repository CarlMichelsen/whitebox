using Microsoft.Extensions.Configuration;

namespace Test;

public static class TestConfiguration
{
    private const string RealIntegrationTestConfigFile = "appsettings.integrationtest.json";
    private const string MockIntegrationTestConfigFile = "appsettings.mock.json";
    
    public static IConfigurationRoot GetTestConfiguration()
    {
        var builder = new ConfigurationBuilder();
        var projectRootPath = GetProjectRootDirectory();
        var realConfigFilePath = Path.Combine(projectRootPath, RealIntegrationTestConfigFile);

        builder.AddJsonFile(
            File.Exists(realConfigFilePath) ? RealIntegrationTestConfigFile : MockIntegrationTestConfigFile,
            optional: false);

        builder.AddEnvironmentVariables();
        return builder.Build();
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