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
    }
}
