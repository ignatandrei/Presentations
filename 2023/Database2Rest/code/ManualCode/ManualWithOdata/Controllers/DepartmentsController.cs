using DalManual;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace ManualWithOdata.Controllers;

public class DepartmentsController : ODataController
{
    private readonly TestsDatabase2RestContext context;

    public DepartmentsController(TestsDatabase2RestContext context)
    {
        this.context = context;
    }
    //https://localhost:7204/odata/Departments?$select=Name
    [EnableQuery]
    [HttpGet("odata/Departments")]
    public ActionResult<IQueryable<Department>> Get()
    {
        return Ok(context.Department);
    }
}
