/// <summary>
/// HTML to PDF converter using Playwright
/// </summary>
internal class HtmlToPDF : IAsyncDisposable
{
    private readonly PdfGenerator _pdfGenerator;
    private readonly ILogger<HtmlToPDF> _logger;
    private bool _disposed = false;

    public HtmlToPDF(ILogger<HtmlToPDF> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _pdfGenerator = new PdfGenerator(_logger);
        _logger.LogDebug("HtmlToPDF service initialized");
    }

    [McpServerTool]
    [Description("Generates a pdf from a html template")]
    public async Task<byte[]> GetPDF(string htmlTemplate)
    {
        if (string.IsNullOrEmpty(htmlTemplate))
        {
            _logger.LogError("HTML template is null or empty");
            throw new ArgumentException("HTML template cannot be null or empty", nameof(htmlTemplate));
        }

        _logger.LogInformation("Starting PDF generation from HTML template");
        _logger.LogDebug("HTML template length: {TemplateLength} characters", htmlTemplate.Length);
        
        try
        {
            var result = await _pdfGenerator.GeneratePdfFromHtml(htmlTemplate);
            _logger.LogInformation("PDF generation completed successfully. Size: {PdfSize} bytes", result.Length);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to generate PDF from HTML template");
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
            _logger.LogDebug("Disposing HtmlToPDF service");
            if (_pdfGenerator != null)
            {
                await _pdfGenerator.DisposeAsync();
            }
            _disposed = true;
            _logger.LogDebug("HtmlToPDF service disposed");
        }
    }
}
