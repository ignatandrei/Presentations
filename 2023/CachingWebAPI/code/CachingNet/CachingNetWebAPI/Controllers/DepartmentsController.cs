
namespace CachingNetWebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly TestContext _context;
        private readonly CachingDepts cachingDepts;

        public DepartmentsController(TestContext context, CachingDepts cachingDepts)
        {
            _context = context;
            this.cachingDepts = cachingDepts;
        }

        // GET: api/Departments
        [HttpGet]
        public async Task<Department[]> GetDepartmentsWithCaching()
        {
            return await this.cachingDepts.GetDepartmentsWithCaching();
        }

        // GET: api/Departments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Department>> GetDepartment(long id)
        {
          if (_context.Departments == null)
          {
              return NotFound();
          }
            var department = await _context.Departments.FindAsync(id);

            if (department == null)
            {
                return NotFound();
            }

            return department;
        }

        // PUT: api/Departments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDepartment(long id, Department department)
        {
            if (id != department.IdDept)
            {
                return BadRequest();
            }

            _context.Entry(department).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DepartmentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Departments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Department>> PostDepartment(Department department)
        {
          if (_context.Departments == null)
          {
              return Problem("Entity set 'TestContext.Departments'  is null.");
          }
            _context.Departments.Add(department);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDepartment", new { id = department.IdDept }, department);
        }

        // DELETE: api/Departments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(long id)
        {
            if (_context.Departments == null)
            {
                return NotFound();
            }
            var department = await _context.Departments.FindAsync(id);
            if (department == null)
            {
                return NotFound();
            }

            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DepartmentExists(long id)
        {
            return (_context.Departments?.Any(e => e.IdDept == id)).GetValueOrDefault();
        }
    }
}
