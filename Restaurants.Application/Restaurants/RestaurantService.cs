
using AutoMapper;
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
        private readonly IMapper _mapper;

        public RestaurantService(IRestaurantRepository restaurantRepository, ILogger<RestaurantService> logger, IMapper mapper)
        {
            _restaurantRepository = restaurantRepository;
            _logger = logger;
            _mapper = mapper;
        }


        public async Task<IEnumerable<RestaurantDTO?>> GetAll()
        {
            _logger.LogInformation("Getting All Restaurants :-) ");
            var restaurants = await _restaurantRepository.GetAll();
            // var restaurantsDTOList = restaurants.Select(RestaurantDTO.FromEntity);
            var restaurantsDTOList = _mapper.Map<IEnumerable<RestaurantDTO>>(restaurants);

            return restaurantsDTOList;
        }
        public async Task<RestaurantDTO?> Get(int id)
        {
            if (id <= 0)
                return null;
            _logger.LogInformation($"Getting Restaurant with id = {id}");
            var restaurant = await _restaurantRepository.Get(id);
            //var restaurantDTO = RestaurantDTO.FromEntity(restaurant);
            var restaurantDTO = _mapper.Map<RestaurantDTO?>(restaurant);
            return restaurantDTO;
        }
    }
}
