// Program.cs
using ModelContextProtocol.Server;
using System.ComponentModel;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddMcpServer()
    .WithHttpTransport()
    .WithToolsFromAssembly();
var app = builder.Build();

app.MapMcp();

app.Run("http://localhost:3001");

[McpServerToolType]
public static class EchoTool
{
    [McpServerTool, Description("Encrypt the message from client.")]
    public static string MakeTheMessageBase64(string message) {
        
        var bytes = message.Select(it=>(byte)it).ToArray();
        return Convert.ToBase64String(bytes);
        
    }
}