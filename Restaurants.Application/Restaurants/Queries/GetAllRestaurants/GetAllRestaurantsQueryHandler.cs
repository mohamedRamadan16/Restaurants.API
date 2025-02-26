
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Common;
using Restaurants.Application.Restaurants.DTOs;
using Restaurants.Domain.Entities;
using Restaurants.Domain.IRepositories;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurants;

internal class GetAllRestaurantsQueryHandler : IRequestHandler<GetAllRestaurantsQuery, PagedResults<RestaurantDTO>>
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

    public async Task<PagedResults<RestaurantDTO>> Handle(GetAllRestaurantsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting All Restaurants :-) ");

        IEnumerable<Restaurant?> restaurants;
        int totalCount = 0;

        if(request.searchQuery == null || request.searchQuery == "")
            restaurants = await _restaurantRepository.GetAll();
        else
            (restaurants, totalCount) = await _restaurantRepository.GetAllMatching(request.searchQuery, request.pageNumber, request.pageSize);

        var restaurantsDTOList = _mapper.Map<IEnumerable<RestaurantDTO>>(restaurants);
        var result = new PagedResults<RestaurantDTO>(restaurantsDTOList, request.pageNumber, request.pageSize, totalCount);

        return result;
    }
}
