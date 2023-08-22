using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using WebAppTutorial.Models;

using System.ComponentModel.DataAnnotations;
using Shared.Models;
namespace WebAppTutorial.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        { }
        public DbSet<Login> Login { get; set; }
        public DbSet<Company> Company { get; set; }
        public DbSet<UsersRegistration> UsersRegistration { get; set; }
        public DbSet<Campaign> Campaign { get; set; }
        public DbSet<Donor_Campaign> Donor_Campaign { get; set; }
        protected override void OnModelCreating(ModelBuilder ModelBuilder)
        {
         
        }


    }
}