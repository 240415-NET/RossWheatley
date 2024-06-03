using Microsoft.EntityFrameworkCore;
using Practice.Entities;

namespace Practice.Data;

public class PracticeContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Item> Items { get; set; }

    public PracticeContext() { }

    public PracticeContext(DbContextOptions<PracticeContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Item>().ToTable("Items");
        modelBuilder.Entity<User>().ToTable("Users");
    }
}