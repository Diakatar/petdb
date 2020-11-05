using Microsoft.EntityFrameworkCore;

namespace EmployeePets.Models
{
    public sealed class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {}

        public DbSet<AnimalType> AnimalTypes { get; set; }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<Person> Persons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>()
                .HasMany(c => c.Pets)
                .WithOne(x => x.Owner)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}