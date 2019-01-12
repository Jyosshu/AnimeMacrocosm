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

            modelBuilder.Entity<AnimeItem>()
                .HasOne(ai => ai.Series)
                .WithMany(s => s.AnimeItems)
                .HasForeignKey(ai => ai.SeriesId);

            modelBuilder.Entity<MangaItem>()
                .HasOne(mi => mi.Series)
                .WithMany(s => s.MangaItems)
                .HasForeignKey(mi => mi.SeriesId);

            modelBuilder.Entity<AnimeItem>()
                .HasOne(ai => ai.ProductionStudio)
                .WithMany(ps => ps.AnimeItems)
                .HasForeignKey(ai => ai.ProductionId);

            modelBuilder.Entity<AnimeItem>()
                .HasOne(ai => ai.Distributor)
                .WithMany(d => d.AnimeItems)
                .HasForeignKey(ai => ai.DistributorId);

            modelBuilder.Entity<MangaItem>()
                .HasOne(mi => mi.Distributor)
                .WithMany(d => d.MangaItems)
                .HasForeignKey(mi => mi.DistributorId);

            modelBuilder.Entity<AnimeItem>()
                .HasOne(ai => ai.Format)
                .WithMany(f => f.AnimeItems)
                .HasForeignKey(ai => ai.FormatId);

            modelBuilder.Entity<MangaItem>()
                .HasOne(mi => mi.Format)
                .WithMany(f => f.MangaItems)
                .HasForeignKey(mi => mi.FormatId);

            modelBuilder.Entity<Image>()
                .HasOne(i => i.AnimeItem)
                .WithMany(ai => ai.AnimeItemImages)
                .HasForeignKey(i => i.AnimeItemId);

            modelBuilder.Entity<Image>()
                .HasOne(i => i.MangaItem)
                .WithMany(mi => mi.MangaItemImages)
                .HasForeignKey(i => i.MangaItemId);
        }
    }
}
