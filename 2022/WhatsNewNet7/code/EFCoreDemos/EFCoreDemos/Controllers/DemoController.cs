
namespace EFCoreDemos.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class DemoController : ControllerBase
{
    private readonly TestsContext _context;

    public DemoController(TestsContext context)
    {
        _context = context;
    }
    [HttpPost]
    public string ExecuteSalaryIncrease(int add)
    {
        
        var s= _context.Employee
            .Where(it => it.IddepartmentNavigation.Name == "IT")
            .ExecuteUpdate(
            s => s.SetProperty(e => e.Salary, e => e.Salary+ add + e.Salary)
            )
            ;
        //var del = _context.Department
        //    .Where(it => it.Name == "HR")
        //    .ExecuteDelete();

        return "records " + s;
    }
}
