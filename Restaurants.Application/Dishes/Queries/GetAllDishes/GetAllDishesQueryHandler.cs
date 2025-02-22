using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Dishes.Commands.CreateDish;
using Restaurants.Application.Dishes.DTOs;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.IRepositories;

namespace Restaurants.Application.Dishes.Queries.GetAllDishes;

internal class GetAllDishesQueryHandler : IRequestHandler<GetAllDishesQuery, IEnumerable<DishDTO>>
{
    private readonly ILogger<CreateDishCommandHandler> _logger;
    private readonly IRestaurantRepository _restaurantRepository;
    private readonly IMapper _mapper;
    private readonly IDishRepository _dishRepository;
    public GetAllDishesQueryHandler(ILogger<CreateDishCommandHandler> logger, IRestaurantRepository restaurantRepository, IMapper mapper, IDishRepository dishRepository)
    {
        _restaurantRepository = restaurantRepository;
        _logger = logger;
        _mapper = mapper;
        _dishRepository = dishRepository;
    }
    public async Task<IEnumerable<DishDTO>> Handle(GetAllDishesQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting All Dishes For Restaurant : {restaurantId}", request.RestaurantId);
        var restaurant = await _restaurantRepository.Get(request.RestaurantId);
        if (restaurant == null) throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

        // you can also use results = restaurants.Dishes
        var restaurantDishes = await _dishRepository.GetAll(request.RestaurantId);
        var restaurantDishesDTO = _mapper.Map<IEnumerable<DishDTO>>(restaurantDishes);
        return restaurantDishesDTO;
    }
}
