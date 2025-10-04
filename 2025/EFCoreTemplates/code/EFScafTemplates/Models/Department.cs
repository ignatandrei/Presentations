using System;
using System.Collections.Generic;

namespace EFScafTemplates.Models;

//TODO: create interface ( for FK ? )
//TODO: create DTO( for FK ? )
public partial class Department
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
 //andrei start
    
    public static string[] Columns() {
    return [
        "Id",
        "Name",
        ];
    }//end columns
    //andrei end
}
   

