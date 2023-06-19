using System.Linq.Expressions;

using CarService.DBContext;
using CarService.Models;

using Microsoft.EntityFrameworkCore;

namespace CarService.Repository;

public class CarRepository(CarDbContext dbContext) : ICarRepository
{
  public async Task<Reservation?> GetAsync(Guid id) 
    => await dbContext.Set<Reservation>().FirstOrDefaultAsync(x => x.Id == id);

  public async Task AddAsync(Reservation reservation)
  { 
    dbContext.Set<Reservation>().Add(reservation);
    await dbContext.SaveChangesAsync();
  }

  public async Task RemoveAsync(Guid id)
  {
    var reservation = await dbContext.Set<Reservation>().FirstOrDefaultAsync(x => x.Id == id);
    if(reservation is null)
    {
      return;
    }
    dbContext.Set<Reservation>().Remove(reservation);
    await dbContext.SaveChangesAsync();
  }

  public async Task<bool> ExistsAsync(Guid carId)
    => await dbContext.Set<Reservation>().AsNoTracking().AnyAsync(x => x.CarId == carId);

  public async Task<bool> ExistsAsync(Expression<Func<Reservation, bool>> expression)
    => await dbContext.Set<Reservation>().AsNoTracking().AnyAsync(expression);

  public async Task SaveChangesAsync()
    => await dbContext.SaveChangesAsync();
}
