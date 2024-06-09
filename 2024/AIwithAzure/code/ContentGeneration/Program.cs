//https://oai.azure.com/portal/3ed86843333547aab070d2a68de13952/playground
using Azure;
using Azure.AI.OpenAI;

var key = Environment.GetEnvironmentVariable("AZURE_OPENAI_API_KEY");
//key = "";//copy from the link above
if (string.IsNullOrEmpty(key))
{
    Console.WriteLine("Please set the environment variable AZURE_OPENAI_API_KEY");
    return;
}

// Note: the Azure OpenAI client library for .NET is in preview.
// Install the .NET library via NuGet: dotnet add package Azure.AI.OpenAI --prerelease 


OpenAIClient client = new OpenAIClient(
    new Uri("https://openaiazureandrei.openai.azure.com/"),
    new AzureKeyCredential(key));

Response<Completions> completionsResponse = await client.GetCompletionsAsync(
    deploymentOrModelName: "test",
    new CompletionsOptions()
    {
        Prompts = { @"

Write an email stating that the full year I have succesfully made all programming tasks.
Add Design Patterns and Cloud Design Patterns and Clean Code. 
The target audience is my boss that is not tech-savvy but understands systems architecture.

1. What should be the subject line of the email?  

2. What should be the body of the email?" },
        Temperature = (float)1,
        MaxTokens = 350,
        NucleusSamplingFactor = (float)1,
        FrequencyPenalty = (float)0,
        PresencePenalty = (float)0,
        GenerationSampleCount = 1,
    });
Completions completions = completionsResponse.Value;



foreach (var choice in completions.Choices)
{
    Console.WriteLine("-----");
    Console.WriteLine(choice.Text);
}

/*
 Dear [Boss's Name],

I am writing to inform you that I have successfully completed all of my programming tasks for the full year. As you may be aware, these tasks have been an integral part of our company's digital transformation and I am pleased to report that I have accomplished them with great dedication and attention to detail.

I would like to highlight that in addition to completing the assigned tasks, I have also focused on incorporating the latest software development best practices into my work. Specifically, I have delved deeper into Design Patterns, Cloud Design Patterns, and Clean Code principles. These are essential components that drive the efficiency and scalability of our systems architecture.

By implementing Design Patterns, I have ensured that our software is more maintainable, extensible, and follows coding standards. On the other hand, through Cloud Design Patterns, I have considered the unique needs and challenges of cloud-based environments to optimize our application's performance and security. Moreover, by adhering to Clean Code principles, I have improved the readability, modularity, and reliability of our codebase.

I believe that my efforts in integrating these practices have not only contributed to the success of our projects but have also improved the overall quality and future-proofing of our systems. I am committed to continuously learning and applying these concepts in my work to drive innovation and enhance our competitive advantage.

Thank you for providing me with the opportunity to work on these challenging tasks and for your continuous support and guidance throughout the year. I look forward to discussing my progress and discussing new and exciting projects in the coming year.

Sincerely,
[Your Name] 
 */


