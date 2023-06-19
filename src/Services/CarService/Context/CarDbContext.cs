using CarService.Models;

using Microsoft.EntityFrameworkCore;

namespace CarService.DBContext;

public class CarDbContext : DbContext
{
  public DbSet<Reservation> Reservations { get; set; }

  public CarDbContext(DbContextOptions<CarDbContext> dbContextOptions) : base(dbContextOptions)
  {
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<Reservation>()
      .HasKey(x => x.Id);
  }
}
