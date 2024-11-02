using Microsoft.EntityFrameworkCore;
using Taller3_API_Rest.DAL.Entities;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Holiday> Holidays { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Holiday>().ToTable("Holidays");
    }
}
