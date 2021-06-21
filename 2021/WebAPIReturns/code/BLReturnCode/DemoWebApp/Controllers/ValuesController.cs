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
        // try with 1 and 2
        // read more https://weblog.west-wind.com/posts/2020/Feb/24/Null-API-Responses-and-HTTP-204-Results-in-ASPNET-Core
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

        [HttpGet("{id}")]
        public ReplyData<Person> GetWithReply(int id)
        {

            return RetrieveWithReplyFromDatabase(id);
        }

        private ReplyData<Person> RetrieveWithReplyFromDatabase(int id)
        {
            try
            {
                Person p = RetrieveFromDatabase(id);
                if (p == null)
                {
                    var r = new ReplyData<Person>();
                    r.Success = false;
                    r.Message = "Cannot find person with id " + id;
                    return r;
                }
                else
                {
                    var r = new ReplyData<Person>();
                    r.Success = true;
                    r.ReturnObject = p;

                    return r;
                }
            }
            catch (Exception ex)
            {
                var r = new ReplyData<Person>();
                r.Success = false;
                r.Message = ex.Message;
                return r;
            }
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
