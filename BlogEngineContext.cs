using Microsoft.EntityFrameworkCore;
using BlogEngine.Models;
using System.Collections.Generic;

namespace BlogEngine.Data
{
    public class BlogEngineContext : DbContext
    {
        public BlogEngineContext(DbContextOptions<BlogEngineContext> options) : base(options) { }

        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}