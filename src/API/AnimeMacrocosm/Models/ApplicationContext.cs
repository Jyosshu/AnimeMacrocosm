using System;
using Microsoft.EntityFrameworkCore;

namespace AnimeMacrocosm.Models
{
    public class ApplicationContext : DbContext
    {

        // https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        { }

        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Series> Series { get; set; }
        public DbSet<SeriesItem> SeriesItems { get; set; }
        public DbSet<CreatorAuthor> CreatorAuthors { get; set; }
        public DbSet<SeriesCreator> SeriesCreators { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Distributor> Distributors { get; set; }
        public DbSet<ProductionStudio> ProductionStudios { get; set; }
        public DbSet<SeriesImage> SeriesImages { get; set; }
        public DbSet<SeriesItemImage> SeriesItemImages { get; set; }
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

            modelBuilder.Entity<SeriesImage>()
                .HasKey(s => new { s.SeriesId, s.ImageId });

            modelBuilder.Entity<SeriesImage>()
                .HasOne(s => s.Series)
                .WithMany(si => si.SeriesImages)
                .HasForeignKey(s => s.SeriesId);

            modelBuilder.Entity<SeriesItem>()
                .HasOne(si => si.Series)
                .WithMany(s => s.SeriesItems)
                .HasForeignKey(si => si.SeriesId);

            modelBuilder.Entity<SeriesItemImage>()
                .HasKey(s => new { s.SeriesItemId, s.ImageId });

            modelBuilder.Entity<SeriesItemImage>()
                .HasOne(s => s.SeriesItem)
                .WithMany(si => si.SeriesItemImages)
                .HasForeignKey(s => s.SeriesItemId);

            //modelBuilder.Entity<SeriesItemImage>()
            //    .HasKey(s => new { s.SeriesItemId, s.ImageId });

            //modelBuilder.Entity<SeriesItemImage>()
            //    .HasOne(s => s.SeriesItem)
            //    .WithMany(i => i.SeriesItemImages)
            //    .HasForeignKey(i => i.SeriesItemId);
        }
    }
}
