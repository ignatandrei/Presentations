namespace MCP_PDF.Tools;


public class ArrayToAny : IAsyncDisposable
{
    private readonly PdfGenerator _pdfGenerator;
    private readonly ILogger<ArrayToAny> _logger;
    private bool _disposed = false;

    public ArrayToAny(ILogger<ArrayToAny> logger) 
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _pdfGenerator = new PdfGenerator(_logger);
        _logger.LogDebug("ArrayToAny service initialized");
    }

    [McpServerTool]
    [Description("Generates a html from a json array serialized as string")]
    public async Task<string> ConvertJsonArrayToHTML([Description("array serialized  as json")] string JsonDataArray)
    {
        _logger.LogInformation("Converting JSON array to HTML");
        try
        {
            var result = await ConvertArrayToHTML(JsonDataArray);
            _logger.LogInformation("JSON array to HTML conversion completed successfully");
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to convert JSON array to HTML");
            throw;
        }
    }

    private ArrayData ConvertFromJsonArray(string JsonDataArray)
    {
        _logger.LogDebug("Parsing JSON array data");
        
        var options = new JsonDocumentOptions()
        {
            AllowTrailingCommas = true,
        };
        var jsonDocument = JsonDocument.Parse(JsonDataArray, options);
        var jsonArray = jsonDocument.RootElement;

        List<string> firstItemProperties = [];
        var isArray = jsonArray.ValueKind == JsonValueKind.Array;
        if (!isArray)
        {
            _logger.LogError("Provided JSON data is not an array. ValueKind: {ValueKind}", jsonArray.ValueKind);
            throw new ArgumentException("Provided JSON data is not an array.");
        }

        var arrayLength = jsonArray.GetArrayLength();
        _logger.LogDebug("JSON array contains {ArrayLength} items", arrayLength);
        
        // Check if the array has at least one element
        if (arrayLength == 0)
        {
            _logger.LogError("Provided JSON array is empty");
            throw new ArgumentException("Provided JSON array is empty.");
        }

        {
            var firstItem = jsonArray[0];

            // Extract all properties from the first item
            if (firstItem.ValueKind == JsonValueKind.Object)
            {
                foreach (var property in firstItem.EnumerateObject())
                {
                    firstItemProperties.Add(property.Name);
                }
                _logger.LogDebug("Extracted {PropertyCount} properties from first item: {Properties}", 
                    firstItemProperties.Count, firstItemProperties);
            }
        }
        ArrayData arrayData = new(firstItemProperties.ToArray(), jsonArray.EnumerateArray().ToArray());
        return arrayData;
    }

    public async Task<string> ConvertArrayToHTML([Description("array serialized  as json")]string JsonDataArray)
    {
        _logger.LogDebug("Starting array to HTML conversion");
        
        ArrayData arrayData = ConvertFromJsonArray(JsonDataArray);
        ArrayTemplate arrayTemplate = new(arrayData);

        // Generate HTML content from the template
        _logger.LogDebug("Rendering HTML template");
        string htmlContent = await arrayTemplate.RenderAsync();
        
        _logger.LogDebug("HTML template rendered successfully. Length: {HtmlLength} characters", htmlContent.Length);
        return htmlContent.Trim();
    }
    [McpServerTool]
    [Description("Generates a pdf from a json array serialized string and save to pdfFileName")]
    public async Task SaveArrayToPDF(string pdfFileName,string JsonDataArray)
    {
        _logger.LogInformation($"Converting JSON array length {JsonDataArray.Length} to {pdfFileName}");
        try
        {
            var pdfBytes = await ConvertArrayToPDF(JsonDataArray);
            await File.WriteAllBytesAsync(pdfFileName, pdfBytes);

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Failed to save JSON array to {pdfFileName}");
            throw;
        }
    }

    //[McpServerTool]
    //[Description("Generates a pdf from a json array serialized string and returns it as byte array ")]
    public async Task<byte[]> ConvertArrayToPDF(string JsonDataArray)
    {
        _logger.LogInformation($"Converting JSON array length {JsonDataArray.Length} to PDF ");
        try
        {
            var htmlContent = await ConvertArrayToHTML(JsonDataArray);
            // Convert HTML to PDF using the shared PDF generator
            byte[] pdfBytes = await _pdfGenerator.GeneratePdfFromHtml(htmlContent);


            _logger.LogInformation($"PDF size: {pdfBytes.Length} bytes");
            return pdfBytes;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to convert JSON array to PDF");
            throw;
        }
    }

    [McpServerTool]
    [Description("Generates a csv from a json array serialized as string")]
    public async Task<string> ConvertArrayToCSV(string JsonDataArray)
    {
        _logger.LogInformation($"Converting JSON array length {JsonDataArray.Length} to CSV");
        try
        {
            var data = ConvertFromJsonArray(JsonDataArray);
            // Convert HTML to PDF using the shared PDF generator
            ArrayCSVTemplate arrayCSVTemplate = new(data);
            var result = await arrayCSVTemplate.RenderAsync();
            
            _logger.LogInformation("JSON array to CSV conversion completed successfully. CSV length: {CsvLength} characters", result.Length);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to convert JSON array to CSV");
            throw;
        }
    }

    public async ValueTask DisposeAsync()
    {
        await DisposeAsyncCore();
        GC.SuppressFinalize(this);
    }

    protected virtual async ValueTask DisposeAsyncCore()
    {
        if (!_disposed)
        {
            _logger.LogDebug("Disposing ArrayToAny service");
            if (_pdfGenerator != null)
            {
                await _pdfGenerator.DisposeAsync();
            }
            _disposed = true;
            _logger.LogDebug("ArrayToAny service disposed");
        }
    }
}
