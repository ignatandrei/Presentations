using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetCoreSimpleBusinessLogic;

namespace NetCoreSimple.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SimpleDivController : ControllerBase
    {

        [HttpGet]
        public string Ping()
        {
            Console.WriteLine(nameof(Ping));
            return "pong";
        }
        [HttpPost]
        public async Task<ActionResult< KeyValuePair<string, int>>> DivAsPOST([FromBody] MyDiv mysum)
        {
            Console.WriteLine(nameof(DivAsPOST));
            try
            {
                var imp = new MyImportantClass();
                var ret = await imp.Divide(mysum.x, mysum.y);
                return new KeyValuePair<string, int>("result", ret);
            }
            catch( Exception ex)
            {
                //TODO: log
                return BadRequest("error:" + ex.Message);
            }
        }
        
        //[HttpGet("{x}/{y}")]
        //public async Task<KeyValuePair<string, int>> GetSum(int x, int y)
        //{
        //    var imp = new MyImportantClass();
        //    var ret = await imp.Divide(x, y);
        //    return new KeyValuePair<string, int>("result", ret);
        //}
    }
}
