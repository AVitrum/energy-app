using Microsoft.EntityFrameworkCore;
using UserApi.Models;

namespace UserApi.Configs;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasIndex(e => e.Username)
            .IsUnique();
        
        modelBuilder.Entity<User>()
            .HasIndex(e => e.Email)
            .IsUnique();
    }
}