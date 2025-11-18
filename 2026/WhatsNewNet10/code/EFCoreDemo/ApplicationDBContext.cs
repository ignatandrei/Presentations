using EFCoreDemo.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
namespace EFCoreDemo.Data;

public partial class ApplicationDbContext : DbContext
{

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>()
            .HasQueryFilter("NoLongerActive", it => it.TerminationDate == null);
    }
}
