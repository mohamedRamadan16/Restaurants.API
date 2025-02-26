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

        public async Task<(IEnumerable<Restaurant>, int)> GetAllMatching(string searchQuery, int pageNumber, int pageSize)
        {
            if (pageNumber <= 0 || pageSize <= 0)
                return ([], 0);

            var searchQueryLower = searchQuery.ToLower();

            var query = _dbContext.Restaurants
                .Where(r => (r.Name.ToLower().Contains(searchQueryLower)) || (r.Description.ToLower().Contains(searchQueryLower)));

            var totalCount = query.Count();

            /// Pagination
            var restaurants = await query.Skip(pageSize * (pageNumber - 1))
                                .Take(pageSize)
                                .ToListAsync();

            return  (restaurants, totalCount);
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
