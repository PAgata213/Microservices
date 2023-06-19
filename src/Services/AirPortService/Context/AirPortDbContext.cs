using AirPortService.Models;

using Microsoft.EntityFrameworkCore;

namespace AirPortService.DBContext;

public class AirPortDbContext : DbContext
{
  public DbSet<Reservation> Reservations { get; set; }

  public AirPortDbContext(DbContextOptions<AirPortDbContext> dbContextOptions) : base(dbContextOptions)
  {
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<Reservation>()
      .HasKey(x => x.Id);
  }
}
