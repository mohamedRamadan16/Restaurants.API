using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Dishes.Commands.CreateDish;
using Restaurants.Application.Dishes.DTOs;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.IRepositories;

namespace Restaurants.Application.Dishes.Queries.GetDishById;

public class GetDishByIdQueryHandler : IRequestHandler<GetDishByIdQuery, DishDTO>
{
    private readonly ILogger<CreateDishCommandHandler> _logger;
    private readonly IRestaurantRepository _restaurantRepository;
    private readonly IMapper _mapper;
    private readonly IDishRepository _dishRepository;
    public GetDishByIdQueryHandler(ILogger<CreateDishCommandHandler> logger, IRestaurantRepository restaurantRepository, IMapper mapper, IDishRepository dishRepository)
    {
        _restaurantRepository = restaurantRepository;
        _logger = logger;
        _mapper = mapper;
        _dishRepository = dishRepository;
    }
    public async Task<DishDTO> Handle(GetDishByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting Dish with Id : {dishId} for Restaurant with Id : {restaurantId}", request.dishId, request.restaurantId);

        var restaurant = await _restaurantRepository.Get(request.restaurantId);
        if (restaurant == null)
            throw new NotFoundException(nameof(Restaurant), request.restaurantId.ToString());

        var dish = await _dishRepository.GetById(request.restaurantId, request.dishId);
        if(dish is null) 
            throw new NotFoundException(nameof(Dish), request.dishId.ToString());

        var dishDto = _mapper.Map<DishDTO>(dish);
        return dishDto;
    }
}
