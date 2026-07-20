using Microsoft.EntityFrameworkCore;
using HelpDeskAPI.Models;

namespace HelpDeskAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

        public DbSet<User> Users => Set<User>();
        public DbSet<Category> Categories => Set<Category>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //User - unique email
            modelBuilder.Entity<User>()
                .HasIndex(c => c.Email)
                .IsUnique();
            //Enum
            modelBuilder.Entity<User>()
                .Property(c => c.Role)
                .HasConversion<string>();
        }
    }
}