using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;
using Restaurants.Domain.IRepositories;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Repositories
{
    internal class DishRepository(RestaurantDbContext _dbContext) : IDishRepository
    {
        public async Task<int> Create(Dish dish)
        {
            await _dbContext.Dishes.AddAsync(dish);
            await _dbContext.SaveChangesAsync();

            return dish.Id;
        }

        public async Task Delete(Dish dish)
        {
            _dbContext.Dishes.Remove(dish);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Dish>> GetAll(int restaurantId)
        {
            var Dishes = await _dbContext.Dishes.Where(d => d.RestaurantId == restaurantId).ToListAsync();
            return Dishes;
        }

        public async Task<Dish?> GetById(int restaurantId, int dishId)
        {
            var Restaurant = await _dbContext.Restaurants.FirstOrDefaultAsync(r => r.Id == restaurantId);
            var Dish = Restaurant?.Dishes.FirstOrDefault(d => d.Id == dishId);
            return Dish;
        }

        public async Task SaveChanges() => await _dbContext.SaveChangesAsync();
    }
}
