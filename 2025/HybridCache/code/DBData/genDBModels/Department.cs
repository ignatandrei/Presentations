using System;
using System.Collections.Generic;

namespace DBData.genDBModels;

//andrei start

partial class DepartmentTable 
{

    public int Id { get; set; }
        public string Name { get; set; }
        
}

//andrei end

public partial class Department
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Employee> Employee { get; set; } = new List<Employee>();
}
