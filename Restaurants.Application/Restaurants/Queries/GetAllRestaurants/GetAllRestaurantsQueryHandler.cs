
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.DTOs;
using Restaurants.Domain.Entities;
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

        IEnumerable<Restaurant?> restaurants;

        if(request.searchQuery == null || request.searchQuery == "")
            restaurants = await _restaurantRepository.GetAll();
        else
            restaurants = await _restaurantRepository.GetAllMatching(request.searchQuery);

        var restaurantsDTOList = _mapper.Map<IEnumerable<RestaurantDTO>>(restaurants);

        return restaurantsDTOList;
    }
}
