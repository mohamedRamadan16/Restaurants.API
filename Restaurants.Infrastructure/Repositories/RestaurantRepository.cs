using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;
using Restaurants.Domain.IRepositories;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Repositories
{
    internal class RestaurantRepository : IRestaurantRepository
    {
        private readonly RestaurantDbContext _dbContext;

        public RestaurantRepository(RestaurantDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<int> CreateAsync(Restaurant restaurant)
        {
            await _dbContext.Restaurants.AddAsync(restaurant);
            await _dbContext.SaveChangesAsync(); 
            return restaurant.Id;
        }
        public async Task<Restaurant?> Get(int id)
        {
            var restaurant = await _dbContext.Restaurants
                .Include(r => r.Dishes)
                .FirstOrDefaultAsync(x => x.Id == id);
            return restaurant;
        }

        public async Task<IEnumerable<Restaurant?>> GetAll()
        {
            return await _dbContext.Restaurants.ToListAsync();
        }

        public async Task<IEnumerable<Restaurant>> GetAllMatching(string searchQuery)
        {
            var searchQueryLower = searchQuery.ToLower();
            return await _dbContext.Restaurants.Where(r => (r.Name.ToLower().Contains(searchQueryLower)) || (r.Description.ToLower().Contains(searchQueryLower))).ToListAsync();
        }

        public async Task Delete(Restaurant restaurant)
        {
            _dbContext.Remove(restaurant);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(Restaurant restaurant)
        {
            _dbContext.Restaurants.Update(restaurant);
            await _dbContext.SaveChangesAsync();
        }

        public async Task SaveChanges() => await _dbContext.SaveChangesAsync();


    }
}
