// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.Hosting;

Console.WriteLine("Hello, World!");
var builder = await RunProgram.DI();
using IHost host = builder.Build();
await host.StartAsync();
Console.WriteLine("Host started");
var test = await RunProgram.testStatic(host);