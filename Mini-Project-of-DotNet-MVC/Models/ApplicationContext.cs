using Microsoft.EntityFrameworkCore;

namespace Mini_Project_of_DotNet_MVC.Models
{
    public class ApplicationContext : DbContext
    {

        public ApplicationContext(DbContextOptions<ApplicationContext> option) : base(option) { }

        public DbSet<Registration> registrations { get; set; }
     }
}
