using Backend.Entities;
using Microsoft.EntityFrameworkCore;

namespace Backend.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<User> Users { get; set; }
    }
}