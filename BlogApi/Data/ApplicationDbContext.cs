using BlogApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogApi.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        //Add data models
        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
