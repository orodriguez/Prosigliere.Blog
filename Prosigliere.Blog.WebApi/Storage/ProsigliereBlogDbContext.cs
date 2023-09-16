using Microsoft.EntityFrameworkCore;
using Prosigliere.Blog.Entities;

namespace Prosigliere.Blog.WebApi.Storage;

public class ProsigliereBlogDbContext : DbContext
{
    public DbSet<Post> Posts { get; set; }
    public DbSet<Comment> Comments { get; set; }

    public ProsigliereBlogDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }
}