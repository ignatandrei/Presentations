



namespace ManualCode.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class ReadmeController : ControllerBase
{
  
    private readonly ILogger<ReadmeController> _logger;

    public ReadmeController(ILogger<ReadmeController> logger)
    {
        _logger = logger;
    }
    public string Scaffold()
    {
        return Readme.Scaffold();
    }
}