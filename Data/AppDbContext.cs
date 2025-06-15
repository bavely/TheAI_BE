using Microsoft.EntityFrameworkCore;
using THEAI_BE.Models;

namespace THEAI_BE.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        // Set users
        public DbSet<User> Users => Set<User>(); 
        // Set threads
        public DbSet<Threads> Threads => Set<Threads>();
        // Set messages
        public DbSet<Messages> Messages => Set<Messages>();
        
 

        // Seed users
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Name = "Alice",
                    Email = "alice@example.com",
                    Auth0Id = "alice",
                    CreatedAt = new DateTime(2024, 01, 01, 0, 0, 0, DateTimeKind.Utc)
                },
                new User
                {
                    Id = 2,
                    Name = "Bob",
                    Email = "bob@example.com",
                    Auth0Id = "bob",
                    CreatedAt = new DateTime(2024, 01, 01, 0, 0, 0, DateTimeKind.Utc)
                },
                new User
                {
                    Id = 3,
                    Name = "Charlie",
                    Email = "charlie@example.com",
                    Auth0Id = "charlie",
                    CreatedAt = new DateTime(2024, 01, 01, 0, 0, 0, DateTimeKind.Utc)
                }
            );
        }
    }
}
