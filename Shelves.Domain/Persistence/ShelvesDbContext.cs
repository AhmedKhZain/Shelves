using Microsoft.EntityFrameworkCore;

namespace Shelves.Domain.Persistence;

public class ShelvesDbContext : DbContext
{
    public ShelvesDbContext(DbContextOptions<ShelvesDbContext> options) : base(options)
    {
    }
    public DbSet<Client> Clients { get; set; } = null!;
    public DbSet<Book> Books { get; set; } = null!;
    public DbSet<Reservation> Reservations { get; set; } = null!;

    override protected void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ShelvesDbContext).Assembly);
    }

}
