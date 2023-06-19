using System.Linq.Expressions;

using HotelService.Models;

namespace HotelService.Repository;

public interface IHotelRepository
{
  Task<Reservation?> GetAsync(Guid id);
  Task AddAsync(Reservation reservation);
  Task RemoveAsync(Guid id);
  Task<bool> ExistsAsync(Guid flyId);
  Task<bool> ExistsAsync(Expression<Func<Reservation, bool>> expression);
  Task SaveChangesAsync();
}
