using Aspire.Hosting.ApplicationModel;
//shameless copy from https://github.com/davidfowl/AspireSwaggerUI
namespace AddAutomation;

public class SwaggerUIAnnotation(string[] documentNames, string path, EndpointReference endpointReference) : IResourceAnnotation
{
    public string[] DocumentNames { get; } = documentNames;
    public string Path { get; } = path;
    public EndpointReference EndpointReference { get; } = endpointReference;
}

