using AT_BL;
using AT_DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AutoTracing.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly ILogger<PersonController> _logger;

        public PersonController(ILogger<PersonController> logger)
        {
            _logger = logger;
        }

        [HttpGet("id")]
        public Task<Person[]> Get([FromServices] PersonRepository p, string id)
        {
            //using var s = Activity.Current?.Source?.StartActivity("WWW_Get_PersonRepository", ActivityKind.Client);
            using var s = new ActivitySource("Andrei").StartActivity("WWW_Get_PersonRepository", ActivityKind.Client);
            
            s?.SetTag("peer.service", "UseAnyName");

            return p.SearchAndLoadData(id);
        }
    }
}
