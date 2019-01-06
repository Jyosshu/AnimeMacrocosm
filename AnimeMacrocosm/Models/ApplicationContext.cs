using System;
using Microsoft.EntityFrameworkCore;

namespace AnimeMacrocosm.Models
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        { }

        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Series> Series { get; set; }
        public DbSet<AnimeItem> AnimeItems { get; set; }
        public DbSet<MangaItem> MangaItems { get; set; }
        public DbSet<CreatorAuthor> CreatorAuthors { get; set; }
        public DbSet<SeriesCreator> SeriesCreators { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Distributor> Distributors { get; set; }
        public DbSet<ProductionStudio> ProductionStudios { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Format> Formats { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SeriesCreator>()
                .HasKey(s => new { s.CreatorId, s.SeriesId });

            modelBuilder.Entity<SeriesCreator>()
                .HasOne(s => s.Series)
                .WithMany(sc => sc.SeriesCreators)
                .HasForeignKey(s => s.SeriesId);

            modelBuilder.Entity<SeriesCreator>()
                .HasOne(sc => sc.CreatorAuthor)
                .WithMany(c => c.SeriesCreators)
                .HasForeignKey(sc => sc.CreatorId);
        }
    }
}
