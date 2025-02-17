
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.DTOs;
using Restaurants.Domain.Entities;
using Restaurants.Domain.IRepositories;

namespace Restaurants.Application.Restaurants
{
    internal class RestaurantService : IRestaurantService
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly ILogger<RestaurantService> _logger;

        public RestaurantService(IRestaurantRepository restaurantRepository, ILogger<RestaurantService> logger)
        {
            _restaurantRepository = restaurantRepository;
            _logger = logger;
        }


        public async Task<IEnumerable<RestaurantDTO?>> GetAll()
        {
            _logger.LogInformation("Getting All Restaurants :-) ");
            var restaurants = await _restaurantRepository.GetAll();
            var restaurantsDTOList = restaurants.Select(RestaurantDTO.FromEntity);

            return restaurantsDTOList;
        }
        public async Task<RestaurantDTO?> Get(int id)
        {
            if (id <= 0)
                return null;
            _logger.LogInformation($"Getting Restaurant with id = {id}");
            var restaurant = await _restaurantRepository.Get(id);
            var restaurantDTO = RestaurantDTO.FromEntity(restaurant);
            return restaurantDTO;
        }
    }
}
