using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AsyncAwait;
using Microsoft.AspNetCore.Mvc;

namespace WebAsyncAwait.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public async Task<ActionResult<bool>> Get()
        {
            var t = new TwoTasks();
            var res = await t.Await2Task();
            return res;   
        }

        // blocking
        [HttpGet("{id}")]
        public ActionResult<bool> Get(int id)
        {
            var t = new TwoTasks();
            var res = t.Await2Task().Result;
            return res;
        }

        
    }
}
