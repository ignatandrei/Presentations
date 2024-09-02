using Aspire.Hosting.ApplicationModel;
using Aspire.Hosting.Lifecycle;
using Microsoft.AspNetCore.Http;
using Aspire.Hosting;
//shameless copy from https://github.com/davidfowl/AspireSwaggerUI
namespace AddAutomation;

public static class SwaggerUIExtensions
{
    public static IResourceBuilder<ProjectResource> WithSwaggerUI(this IResourceBuilder<ProjectResource> builder,
            string[]? documentNames = null, string path = "swagger/v1/swagger.json", string endpointName = "http")
    {
        if (!builder.ApplicationBuilder.Resources.OfType<SwaggerUIResource>().Any())
        {
            // Add the swagger ui code hook and resource
            builder.ApplicationBuilder.Services.TryAddLifecycleHook<SwaggerUiHook>();
            builder.ApplicationBuilder.AddResource(new SwaggerUIResource("swagger-ui"))
                .WithInitialState(new CustomResourceSnapshot
                {
                    ResourceType = "swagger-ui",
                    Properties = [],
                    State = "Starting"
                })
                .ExcludeFromManifest();
        }

        return builder.WithAnnotation(new SwaggerUIAnnotation(documentNames ?? ["v1"], path, builder.GetEndpoint(endpointName)));
    }


}

