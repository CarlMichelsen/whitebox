namespace Api;

public static class Dependencies
{
    public static void AddDependencies(this WebApplicationBuilder builder)
    {
        // Swagger
        builder.Services
            .AddEndpointsApiExplorer()
            .AddSwaggerGen();
    }
}