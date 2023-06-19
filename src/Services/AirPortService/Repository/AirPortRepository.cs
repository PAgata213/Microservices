using System.Linq.Expressions;

using AirPortService.DBContext;
using AirPortService.Models;

using Microsoft.EntityFrameworkCore;

namespace AirPortService.Repository;

public class AirPortRepository(AirPortDbContext dbContext) : IAirPortRepository
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

  public async Task<bool> ExistsAsync(Guid flyId)
    => await dbContext.Set<Reservation>().AsNoTracking().AnyAsync(x => x.FlyId == flyId);

  public async Task<bool> ExistsAsync(Expression<Func<Reservation, bool>> expression)
    => await dbContext.Set<Reservation>().AsNoTracking().AnyAsync(expression);

  public async Task SaveChangesAsync()
    => await dbContext.SaveChangesAsync();
}
