using EmployeePets.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeePets.Models
{
    public sealed class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<AnimalType> AnimalTypes { get; set; }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<Person> Persons { get; set; }
    }
}