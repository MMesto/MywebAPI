using Microsoft.EntityFrameworkCore;
using MywebAPI.Data.Entities;

namespace MywebAPI.Data;

public class PortfolioDbContext : DbContext
{
    // De opties (welke database, welk adres)
    public PortfolioDbContext(DbContextOptions<PortfolioDbContext> options)
        : base(options)
    {
    }

    // Elke DbSet wordt een tabel in de database
    public DbSet<Project> Projects => Set<Project>();
    public DbSet<Blogpost> Blogposts => Set<Blogpost>();
}