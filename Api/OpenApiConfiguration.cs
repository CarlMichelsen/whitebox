using Application.Configuration;
using Microsoft.OpenApi.Models;

namespace Api;

internal static class OpenApiConfiguration
{
    public static IServiceCollection ConfigureApplicationOpenApi(this IServiceCollection services)
    {
        services.AddOpenApi(options =>
        {
            options.AddDocumentTransformer((document, _, _) =>
            {
                document.Info = new OpenApiInfo
                {
                    Title = $"{ApplicationConstants.Name} Api",
                    Version = ApplicationConstants.Version,
                    Description = "Api for an llm chat frontend called \"WhiteBox\"",
                    Contact = new OpenApiContact
                    {
                        Name = "API Support",
                        Email = "GoodLuck@FakeMail.memes",
                    },
                };
                
                return Task.CompletedTask;
            });
        });

        return services;
    }
}