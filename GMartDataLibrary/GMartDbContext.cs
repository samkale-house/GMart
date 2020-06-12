using System;
using Microsoft.EntityFrameworkCore;
using GMartDataLibrary.Models;
namespace GMartDataLibrary
{
    public class GMartDbContext: DbContext// DbContext  
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public GMartDbContext(DbContextOptions<GMartDbContext> options) : base(options)
        {
            Console.WriteLine("From dbcontext constructor");
        }

        public GMartDbContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            //if not configured
            if (!options.IsConfigured)
            {
                //configure to use sql
                options.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;database=GMartDb;MultipleActiveResultSets=true");
            }
        }

    }
}
