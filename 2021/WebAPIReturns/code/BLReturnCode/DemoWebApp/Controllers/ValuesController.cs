using BLReturnCode;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoWebApp.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // TODO: try with 1 and 2
        [HttpGet("{id}")]
        public Person GetPerson(int id)
        {
            return RetrieveFromDatabase(id);
        }

        // try with 1 and 2
        [HttpGet("{id}")]
        public ActionResult<Person> GetPerson404(int id)
        {
            var p = RetrieveFromDatabase(id);
            if (p == null)
                return NotFound($"{nameof(Person)} with {id} are not found");

            return p;

        }
        //try with id 1 and 2
        [HttpPost]
        public int SavePerson(Person p)
        {
            // save to database, then
            return p.ID;
        }
        private Person RetrieveFromDatabase(int id)
        {
            if (id % 2 == 0)
                return new Person()
                {
                    ID = id,
                    Name = "Andrei Ignat " + id
                    
                };


            return null;
        }
    }
}
