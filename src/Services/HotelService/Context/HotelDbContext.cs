using HotelService.Models;

using Microsoft.EntityFrameworkCore;

namespace HotelService.DBContext;

public class HotelDbContext : DbContext
{
  public DbSet<Reservation> Reservations { get; set; }

  public HotelDbContext(DbContextOptions<HotelDbContext> dbContextOptions) : base(dbContextOptions)
  {
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<Reservation>()
      .HasKey(x => x.Id);
  }
}
