using Restaurants.Domain.Entities;

namespace Restaurants.Domain.IRepositories
{
    public interface IRestaurantRepository
    {
        Task<IEnumerable<Restaurant?>> GetAll();
        Task<Restaurant?> Get(int id);
        Task<int> CreateAsync(Restaurant restaurant);
        Task Delete(Restaurant restaurant);
        Task Update(Restaurant restaurant);
        Task SaveChanges();
    }
}
