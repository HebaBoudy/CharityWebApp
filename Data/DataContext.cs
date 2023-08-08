using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using WebAppTutorial.Models;

namespace WebAppTutorial.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        { }
        public DbSet<Company> Company { get; set; }
        public DbSet<Login> Login { get; set; }
        protected override void OnModelCreating(ModelBuilder ModelBuilder)

        {
            base.OnModelCreating(ModelBuilder);/*LoginID foreign key to CompanyId */

            ModelBuilder.Entity<Company>().HasData(new Company { CompanyId = 1, Name = "DummyName1", TotalRaisedAmount = 1, type = "DummyType1" });
            ModelBuilder.Entity<Company>().HasData(new Company { CompanyId = 2, Name = "DummyName2", TotalRaisedAmount = 1, type = "DummyType2" });
            ModelBuilder.Entity<Login>().HasData(new Login { Id = 1, UserName = "UserName1", Password = "Password1" });
            ModelBuilder.Entity<Login>().HasData(new Login {Id = 2, UserName = "UserName2", Password = "Password2" });
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder options)
        //{
        //    options.UseSqlServer(@"Data Source=.\SQLEXPRESS;Initial Catalog=WebAppTutorialDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        //}

    }
}