using ClientAppsIntegration.WinForms;

namespace ZipAsAService.WinForm;

public partial class Form1 : Form
{
    private readonly ILogger _logger;
    private readonly WeatherApiClient _weatherApiClient;
    private readonly CancellationTokenSource _closingCts = new();

    public Form1(ILogger<Form1> logger, WeatherApiClient weatherApiClient)
    {
        InitializeComponent();
        _logger = logger;
        _weatherApiClient = weatherApiClient;
    }

    private async void btnLoadWeather_Click(object sender, EventArgs e)
    {
        btnLoadWeather.Enabled = false;
        pbLoading.Visible = true;

        try
        {

            var weather = await _weatherApiClient.GetWeatherAsync(_closingCts.Token);
            dgWeather.DataSource = weather;
        }
        catch (TaskCanceledException)
        {
            return;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading weather");

            dgWeather.DataSource = null;
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        pbLoading.Visible = false;
        btnLoadWeather.Enabled = true;
    }

    private async void button1_Click(object sender, EventArgs e)
    {
        try
        {
            button1.Enabled = false;
            var data = await _weatherApiClient.GetZipFile(txtZip.Text);
            var tempFiler= Path.GetTempPath();
            var path= Path.Combine(tempFiler, "test.zip");
            File.WriteAllBytes(path, data);
            MessageBox.Show($"File saved to {path}", "File Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading zip");

            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }
        button1.Enabled = true;
    }
}
