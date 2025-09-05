namespace MCP_PDF.Tools;

/// <summary>
/// Shared PDF generator using Playwright for HTML to PDF conversion
/// </summary>
internal class PdfGenerator : IAsyncDisposable
{
    private IBrowser? _browser;
    private IPlaywright? _playwright;
    private readonly Microsoft.Extensions.Logging.ILogger _logger;
    private bool _disposed = false;

    public PdfGenerator(Microsoft.Extensions.Logging.ILogger logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _logger.LogDebug("PdfGenerator initialized");
    }

    /// <summary>
    /// Generates a PDF from HTML content
    /// </summary>
    /// <param name="htmlContent">The HTML content to convert to PDF</param>
    /// <returns>PDF as byte array</returns>
    public async Task<byte[]> GeneratePdfFromHtml(string htmlContent)
    {
        _logger.LogDebug("Starting PDF generation from HTML content");
        
        await EnsureBrowserInitialized();
        
        var page = await _browser!.NewPageAsync();
        try
        {
            _logger.LogDebug("Setting page content");
            await page.SetContentAsync(htmlContent);
            
            _logger.LogDebug("Generating PDF with A4 format and standard margins");
            // Generate PDF with standard options
            var pdfBytes = await page.PdfAsync(new PagePdfOptions
            {
                Format = "A4",
                PrintBackground = true,
                Margin = new Margin
                {
                    Top = "1cm",
                    Right = "1cm",
                    Bottom = "1cm",
                    Left = "1cm"
                }
            });
            
            _logger.LogDebug("PDF generated successfully. Size: {PdfSize} bytes", pdfBytes.Length);
            return pdfBytes;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to generate PDF from HTML content");
            throw;
        }
        finally
        {
            _logger.LogDebug("Closing browser page");
            await page.CloseAsync();
        }
    }

    private async Task EnsureBrowserInitialized()
    {
        if (_browser == null)
        {
            _logger.LogDebug("Initializing Playwright browser");
            _playwright = await Playwright.CreateAsync();
            _browser = await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = true
            });
            _logger.LogInformation("Playwright browser initialized successfully");
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
            _logger.LogDebug("Disposing PdfGenerator");
            if (_browser != null)
            {
                _logger.LogDebug("Disposing browser");
                await _browser.DisposeAsync();
            }
            
            if (_playwright != null)
            {
                _logger.LogDebug("Disposing Playwright");
                _playwright.Dispose();
            }
            
            _disposed = true;
            _logger.LogDebug("PdfGenerator disposed successfully");
        }
    }
}