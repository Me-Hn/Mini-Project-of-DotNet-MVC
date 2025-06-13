using Microsoft.EntityFrameworkCore;
using Mini_Project_of_DotNet_MVC.Models;
using System.Reflection.Emit;

namespace Mini_Project_of_DotNet_MVC.Models
{
    public class ApplicationContext : DbContext
    {

        public ApplicationContext(DbContextOptions<ApplicationContext> option) : base(option) { }

        public DbSet<Registration> registrations { get; set; }

        public DbSet<VehicleRegistration> VehicleRegistrations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Registration>()
                .Ignore(r => r.ConfirmPassword); // Explicitly ignore

            //for Vehicle Registration
            // Configure the table name (optional)
            modelBuilder.Entity<VehicleRegistration>().ToTable("VehicleRegistrations");

            // Configure default values for CreatedAt and ExpiryDate
            modelBuilder.Entity<VehicleRegistration>()
                .Property(v => v.CreatedAt)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<VehicleRegistration>()
                .Property(v => v.ExpiryDate)
                .HasComputedColumnSql("DATEADD(year, 1, CreatedAt)");

        }
      
    }
}


