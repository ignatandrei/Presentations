using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("all")]
    public class Dep : Controller
    {
        EmpContext Context { get; set; }

        public Dep(EmpContext context)
        {
            Context = context;
        }

        // GET: api/values
        [HttpGet]
        public async Task<Tuple<bool, IEnumerable<Department>>> Get()
        {
            try
            {


                //var list = new List<Department>();

                //foreach (var department in Context.Department.Include(d => d.Employee))
                //{
                //    foreach (var employee in department.Employee)
                //    {
                //        employee.IddepartmentNavigation = null;
                //    }
                //    list.Add(department);
                //}
                var list = await Context.Department.ToArrayAsync();
                return new Tuple<bool, IEnumerable<Department>>(true, list);
            }
            catch (Exception ex)
            {
                //TODO: log the exception
                return new Tuple<bool, IEnumerable<Department>>(false,null);
            }
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public Tuple<bool, Department> Get(int id)
        {
            try
            {
                return new Tuple<bool, Department>(true, Context.Department.First(it => it.Id == id));
            }
            catch (Exception ex)
            {
                //TODO: log the exception
                return new Tuple<bool, Department>(false, null);
            }
        }

        // POST api/values
        [HttpPost]
        public async Task<Tuple<bool,Department>>  Post([FromBody]Department value)
        {
            try
            {
                if (value.Id == 0)
                {
                    Context.Department.Add(value);
                }
                else
                {
                    Context.Department.Attach(value);
                    Context.Entry(value).State= EntityState.Modified;
                }
                await Context.SaveChangesAsync();
                return new Tuple<bool, Department>(true, value);
            }
            catch (Exception ex)
            {
                //TODO: log the exception
                return new Tuple<bool, Department>(false, null);
            }
        }

        //// PUT api/values/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<Tuple<bool, Department>> Delete(int id)
        {
            try
            {
                Department d = await Context.Department.FirstAsync(it => it.Id == id);
                Context.Department.Remove(d);
                await Context.SaveChangesAsync();
                return new Tuple<bool, Department>(true, d);
            }
            catch (Exception ex)
            {
                //TODO: log the exception
                return new Tuple<bool, Department>(false,null);
            }

}
    }
}
