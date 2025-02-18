
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.DTOs;
using Restaurants.Domain.IRepositories;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurants;

internal class GetAllRestaurantsQueryHandler : IRequestHandler<GetAllRestaurantsQuery, IEnumerable<RestaurantDTO>>
{
    private readonly IRestaurantRepository _restaurantRepository;
    private readonly ILogger<GetAllRestaurantsQueryHandler> _logger;
    private readonly IMapper _mapper;

    public GetAllRestaurantsQueryHandler(IRestaurantRepository restaurantRepository, ILogger<GetAllRestaurantsQueryHandler> logger, IMapper mapper)
    {
        _restaurantRepository = restaurantRepository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<IEnumerable<RestaurantDTO>> Handle(GetAllRestaurantsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting All Restaurants :-) ");
        var restaurants = await _restaurantRepository.GetAll();
        var restaurantsDTOList = _mapper.Map<IEnumerable<RestaurantDTO>>(restaurants);

        return restaurantsDTOList;
    }
}
