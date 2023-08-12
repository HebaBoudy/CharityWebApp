using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using WebAppTutorial.Models;
using System.ComponentModel.DataAnnotations;
namespace WebAppTutorial.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        { }
       
        public DbSet<Login> Login { get; set; }
        public DbSet<Company> Company { get; set; }
        public DbSet<UsersRegistration> UsersRegistration { get; set; }
      

        protected override void OnModelCreating(ModelBuilder ModelBuilder)

        {
            //base.OnModelCreating(ModelBuilder);/*LoginID foreign key to CompanyId */
            //ModelBuilder.Entity<Company>().
            //     HasOne(c => c.Login)
            //    .WithOne(L => L.Company)
            //    .HasForeignKey<Login>(L => L.ID)
            //    .IsRequired();

            //ModelBuilder.Entity<UsersRegistration>().
            //    HasOne(c => c.UsersLogin)
            //   .WithOne(L => L.UsersRegistration)
            //   .HasForeignKey<UsersLogin>(L => L.ID)
            //   .IsRequired();

            //ModelBuilder.Entity<Company>().HasData(new Company {ID=1,  Name = "DummyName1", TotalRaisedAmount = 1, type = "DummyType1" });
            //ModelBuilder.Entity<Company>().HasData(new Company { ID=2, Name = "DummyName2", TotalRaisedAmount = 1, type = "DummyType2" });
            //ModelBuilder.Entity<Company>().HasData(new Company { ID = 3, Name = "DummyName3", TotalRaisedAmount = 3, type = "DummyType3" });
            //ModelBuilder.Entity<Login>().HasData(new Login { ID=1, UserName = "UserName1", Password = "Password1" });

            //ModelBuilder.Entity<Login>().HasData(new Login { ID=2, UserName = "UserName2", Password = "Password2" });
            //ModelBuilder.Entity<Login>().HasData(new Login { ID = 3, UserName = "UserName3", Password = "Password3" });
            //ModelBuilder.Entity<UsersRegistration>().HasData(new Models.UsersRegistration {ID=1,FName = "Heba", LName = "Gamal", PhoneNo = "01152523590", Email = "hebaboudy11@gmail.com" });
            //ModelBuilder.Entity<UsersRegistration>().HasData(new Models.UsersRegistration {ID=2, FName = "Alaa", LName = "Mohammed", PhoneNo = "01002210022", Email = "alaa@gmail.com"  });

        }


    }
}