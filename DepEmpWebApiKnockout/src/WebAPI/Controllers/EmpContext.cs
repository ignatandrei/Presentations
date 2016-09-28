using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Models
{
    public partial class EmpContext
    {
        public EmpContext(DbContextOptions<EmpContext> options)
           : base(options)
        { }
    }
}
