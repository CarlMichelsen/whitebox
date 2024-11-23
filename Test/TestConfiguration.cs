using Microsoft.Extensions.Configuration;

namespace Test;

public static class TestConfiguration
{
    public static IConfigurationRoot GetTestConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.integrationtest.json", optional: false)
            .AddEnvironmentVariables();
        return builder.Build();
    }
}