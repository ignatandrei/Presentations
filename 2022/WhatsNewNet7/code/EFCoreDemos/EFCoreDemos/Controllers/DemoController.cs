
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
    public string ExecuteUpdate(int id)
    {
        var s= _context.Departments
            .Where(it => it.Name.Contains("T"))
            .ExecuteUpdate(
            s => s.SetProperty(e => e.Name, e => "Modified" + e.Name)
            )
            ;
        return "records " + s;
    }
}
