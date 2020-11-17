using Model.DataModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
  public  class ProjectDbContext : DbContext
    {
        public ProjectDbContext() : base("name=ProjectDb")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ProjectDbContext, Migrations.Configuration>("ProjectDb"));
        }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Category> Categorys { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Oders> Oderss { get; set; }
        public DbSet<Oders_Detail> Oders_Details { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
    }
}
