using Microsoft.EntityFrameworkCore;
using HelpDeskAPI.Models;

namespace HelpDeskAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

        public DbSet<User> Users => Set<User>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Ticket> Tickets => Set<Ticket>();
        public DbSet<TicketReply> TicketReplies => Set<TicketReply>();

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

            // Ticket -> Category (many-to-one)
            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Category)
                .WithMany(c => c.Tickets)
                .HasForeignKey(t => t.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            // Ticket -> CreatedBy (many-to-one)
            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.CreatedBy)
                .WithMany(u => u.CreatedTickets)
                .HasForeignKey(t => t.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);

            // Ticket -> AssignedTo (many-to-one)
            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.AssignedTo)
                .WithMany(u => u.AssignedTickets)
                .HasForeignKey(t => t.AssignedToId)
                .OnDelete(DeleteBehavior.SetNull);

            // TicketReply -> Ticket (many-to-one)
            modelBuilder.Entity<TicketReply>()
                .HasOne(tr => tr.Ticket)
                .WithMany(t => t.Replies)
                .HasForeignKey(tr => tr.TicketId)
                .OnDelete(DeleteBehavior.Cascade);

            // TicketReply -> User (many-to-one)
            modelBuilder.Entity<TicketReply>()
                .HasOne(tr => tr.User)
                .WithMany(u => u.Replies)
                .HasForeignKey(tr => tr.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}