﻿using Microsoft.EntityFrameworkCore;

namespace AsyncEnumerable
{
    public class MyDataContext : DbContext
    {
        public MyDataContext()
        { }

        public MyDataContext(DbContextOptions<MyDataContext> options)
            : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=EFProviders.InMemory;Trusted_Connection=True;ConnectRetryCount=0");
                optionsBuilder.UseInMemoryDatabase(databaseName: "testing");
            }
            //TODO: add configuring to table
        }
        public DbSet<PersonWithBlog> PersonWithBlog { get; set; }
    }
}
