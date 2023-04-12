using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Versioning.Controllers
{
    [ApiVersion("1.0",Deprecated =true)]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    [ApiController]
    public class VersionExampleController : ControllerBase
    {

        public string Version1Or2()
        {
            return "Version 1 or 2";
        }

        [HttpGet()]
        [MapToApiVersion("1.0")]
        public string Name()
        {
            return "Andrei Ignat v1";
        }


        [HttpGet()]
        [ActionName("Name")]
        [MapToApiVersion("2.0")]
        public string NameVersion2()
        {
            return "Andrei Ignat version 2";
        }

    }
}
