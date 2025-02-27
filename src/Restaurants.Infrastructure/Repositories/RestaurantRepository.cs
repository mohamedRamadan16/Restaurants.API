using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.IRepositories;
using Restaurants.Infrastructure.Persistence;
using System.Linq.Expressions;

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

        public async Task<(IEnumerable<Restaurant>, int)> GetAllMatching(string? searchQuery, int pageNumber, int pageSize, string? sortBy, SortOption sortOption = SortOption.Ascending)
        {
            if (pageNumber <= 0 || pageSize <= 0)
                return ([], 0);

            IQueryable<Restaurant>? query;
            if (String.IsNullOrEmpty(searchQuery))
                query = _dbContext.Restaurants;
            else
            {
                var searchQueryLower = searchQuery.ToLower();
                query = _dbContext.Restaurants
                    .Where(r => (r.Name.ToLower().Contains(searchQueryLower)) || (r.Description.ToLower().Contains(searchQueryLower)));
            }
            var totalCount = query.Count();

            if(sortBy != null)
            {
                var columnsSelector = new Dictionary<string, Expression<Func<Restaurant, object>>>
                {
                    { nameof(Restaurant.Name), r => r.Name },
                    { nameof(Restaurant.Description), r => r.Description },
                    { nameof(Restaurant.Category), r => r.Category },
                };

                var selector = columnsSelector[sortBy];
                query = (sortOption == SortOption.Ascending) ? query.OrderBy(selector) : query.OrderByDescending(selector);
            }

            /// Pagination
            var restaurants = await query.Skip(pageSize * (pageNumber - 1))
                                .Take(pageSize)
                                .ToListAsync();

            return (restaurants, totalCount);
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
