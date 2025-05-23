using Microsoft.EntityFrameworkCore;
using POWAPI.Models;

namespace POWAPI.Services;

public class StudentDbContext : DbContext
{
    public DbSet<Student> Students { get; init; }

    public StudentDbContext(DbContextOptions options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Student>();
    }
}