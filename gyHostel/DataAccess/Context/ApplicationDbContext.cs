using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Hostel> Hostel { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Booking> Booking { get; set; }
        public DbSet<Review> Review { get; set; }
        public DbSet<Chat> Chat { get; set; }
        public DbSet<Room> Room { get; set; }
        public DbSet<Record> Record { get; set; }
        public DbSet<Finance> Finances { get; set; }

    }
}
