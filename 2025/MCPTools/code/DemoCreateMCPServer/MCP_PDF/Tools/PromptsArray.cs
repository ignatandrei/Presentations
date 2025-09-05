namespace MCP_PDF.Tools;

[McpServerPromptType]
public class PromptsArray
{
    [McpServerPrompt(Name = "array2html"), Description("A prompt to transform an array to html")]
    public static string ArrayToHTML() => """
        Please convert this json into html : [ {"firstName":"John", "lastName":"Doe"}, {"firstName":"Anna", "lastName":"Smith"}, {"firstName":"Peter", "lastName":"Jones"} ] and store in a local file
        """;

    [McpServerPrompt(Name = "array2pdf"), Description("A prompt to transform an array to pdf")]
    public static string ArrayToPDF() => """
        Please convert this json into pdf : [ {"firstName":"John", "lastName":"Doe"}, {"firstName":"Anna", "lastName":"Smith"}, {"firstName":"Peter", "lastName":"Jones"} ] by saving to result.pdf in the current folder
        """
       ;

    [McpServerPrompt(Name = "distant2pdf"), Description("A prompt to transform an json to pdf")]
    public static string DistantArrayToPDF() => """

        Use MCP file system and to find the current folder .

        Please fetch data from https://jsonplaceholder.typicode.com/posts

        and save to posts.pdf in the current folder
        """
       ;
}
