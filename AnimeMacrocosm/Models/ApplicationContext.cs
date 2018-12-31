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
        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<Series> Series { get; set; }
        public DbSet<AnimeItem> AnimeItems { get; set; }
        public DbSet<MangaItem> MangaItems { get; set; }
        public DbSet<CreatorAuthor> CreatorAuthors { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Distributor> Distributors { get; set; }
        public DbSet<ProductionStudio> ProductionStudios { get; set; }
        public DbSet<MediaType> MediaTypes { get; set; }
        public DbSet<Image> Images { get; set; }
    }
}
