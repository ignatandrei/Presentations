using AsyncAwait;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace DemoBlockingFramework.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public async Task<bool> Get()
        {
            var t = new TwoTasks();
            var res = await t.Await2Task();
            return res;

        }

        // GET api/values/5
        public bool Get(int id)
        {
            var t = new TwoTasks();
            var res = t.Await2Task().Result;
            return res;

        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
