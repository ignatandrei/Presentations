using DalManual;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace ManualWithOdata.Controllers;

public class DepartmentsController : ODataController
{
    private readonly TestsDatabase2RestContext context;

    public DepartmentsController(TestsDatabase2RestContext context)
    {
        this.context = context;
    }
    public ActionResult<IQueryable<Department>> Get()
    {
        return Ok(context.Department);
    }
}
