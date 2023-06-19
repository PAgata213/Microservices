using System.Linq.Expressions;

using AirPortService.Models;

namespace AirPortService.Repository;

public interface IAirPortRepository
{
  Task<Reservation?> GetAsync(Guid id);
  Task AddAsync(Reservation reservation);
  Task RemoveAsync(Guid id);
  Task<bool> ExistsAsync(Guid flyId);
  Task<bool> ExistsAsync(Expression<Func<Reservation, bool>> expression);
  Task SaveChangesAsync();
}
