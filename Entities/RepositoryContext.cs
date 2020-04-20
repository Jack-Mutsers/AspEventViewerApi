using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Entities
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options) : base(options) { }

        public DbSet<Artist> Artists { get; set; }
        public DbSet<DatePlanning> DatePlanning { get; set; }
        public DbSet<Event> Event { get; set; }
        public DbSet<EventDate> EventDate { get; set; }
        public DbSet<Genre> Genre { get; set; }
        public DbSet<Preference> Preference { get; set; }
        public DbSet<Rating> Rating { get; set; }
        public DbSet<Review> Review { get; set; }
        public DbSet<Schedule> Schedule { get; set; }
        public DbSet<ScheduleItem> ScheduleItems { get; set; }
        public DbSet<Stage> Stage { get; set; }
        public DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Rating>()
                .HasKey(r => new { r.event_date_id, r.user_id });
        }
    }
}
