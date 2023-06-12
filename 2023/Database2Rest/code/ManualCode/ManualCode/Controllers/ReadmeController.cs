



namespace ManualCode.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class ReadmeController : ControllerBase
{
    private readonly TestsDatabase2RestContext a;
    private readonly ILogger<ReadmeController> _logger;

    public ReadmeController(TestsDatabase2RestContext a, ILogger<ReadmeController> logger)
    {
        this.a = a;
        _logger = logger;
    }
    [HttpGet]
    public string Scaffold()
    {
        return Readme.Scaffold();
    }
}