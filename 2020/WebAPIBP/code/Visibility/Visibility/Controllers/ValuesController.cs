using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Visibility.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        
        //Answer to the right - delete spaces                                                                                                                TODO: put [HttpGet]
        public string Get()
        {
            return "http://msprogrammer.serviciipeweb.ro/2020/10/05/asp-net-core-webapi-should-must-have/";
        }

        [HttpGet("{id}")]
        public string Get(string id)
        {
            throw new ArgumentException($" error {id}");
        }
    }
}
