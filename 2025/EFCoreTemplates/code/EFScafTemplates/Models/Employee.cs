using System;
using System.Collections.Generic;

namespace EFScafTemplates.Models;

//TODO: create interface ( for FK ? )
//TODO: create DTO( for FK ? )
public partial class Employee
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int? DepartmentId { get; set; }

    public virtual Department? Department { get; set; }
 //andrei start
    
    public static string[] Columns() {
    return [
        "Id",
        "Name",
        "DepartmentId",
        ];
    }//end columns
    //andrei end
}
   

