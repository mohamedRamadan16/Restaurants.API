using Restaurants.Domain.Entities;

namespace Restaurants.Domain.IRepositories
{
    public interface IDishRepository
    {
        Task<int> Create(Dish dish);
        Task<IEnumerable<Dish>> GetAll(int restaurantId);
        Task<Dish?> GetById(int restaurantId, int dishId);
        Task Delete(Dish dish);
        Task SaveChanges();
    }
}
