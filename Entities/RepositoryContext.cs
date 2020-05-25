using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Entities
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options) : base(options) { }

        public DbSet<Artist> Artists { get; set; }
        public DbSet<ArtistGenre> artistGenre { get; set; }
        public DbSet<DatePlanning> DatePlanning { get; set; }
        public DbSet<Event> Event { get; set; }
        public DbSet<EventGenre> EventGenre { get; set; }
        public DbSet<EventDate> EventDate { get; set; }
        public DbSet<Genre> Genre { get; set; }
        public DbSet<Preference> Preference { get; set; }
        public DbSet<Review> Review { get; set; }
        public DbSet<Schedule> Schedule { get; set; }
        public DbSet<ScheduleItem> ScheduleItems { get; set; }
        public DbSet<Stage> Stage { get; set; }
        public DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EventGenre>()
                .HasKey(eg => new { eg.event_id, eg.genre_id });

            modelBuilder.Entity<ArtistGenre>()
                .HasKey(eg => new { eg.artist_id, eg.genre_id });


            modelBuilder.Entity<User>().ToTable("User").Property(p => p.id).HasColumnName("Id").ValueGeneratedOnAdd();
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(u => u.username).IsUnique();
            });
        }

    }
}
