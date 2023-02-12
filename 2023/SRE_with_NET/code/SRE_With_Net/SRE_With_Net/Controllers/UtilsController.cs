using Microsoft.AspNetCore.Mvc;

namespace SRE_With_Net.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UtilsController : ControllerBase
    {
        public static CancellationTokenSource cts = new();
        public static bool IsCancellationRequired = false;
        private readonly ILogger<UtilsController> _logger;

        public UtilsController(ILogger<UtilsController> logger)
        {
            _logger = logger;
        }

        //shutdown
        [HttpGet("{seconds}")]
        public void Shutdown(int seconds)
        {
            //TODO: see if there are any other requests in progress
            //or make a middleware to not accept other requests
            IsCancellationRequired = true;
            cts.CancelAfter(seconds*1000);
            
        }
        [HttpGet]
        public int Test()
        {
            return 10;
        }
    }
}