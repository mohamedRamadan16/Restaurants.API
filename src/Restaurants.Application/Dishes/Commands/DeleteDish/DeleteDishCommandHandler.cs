﻿using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Dishes.Commands.CreateDish;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.IRepositories;

namespace Restaurants.Application.Dishes.Commands.DeleteDish;

public class DeleteDishCommandHandler : IRequestHandler<DeleteDishCommand>
{
    private readonly ILogger<CreateDishCommandHandler> _logger;
    private readonly IRestaurantRepository _restaurantRepository;
    private readonly IMapper _mapper;
    private readonly IDishRepository _dishRepository;
    private readonly IRestaurantAuthorizationService restaurantAuthorizationService;

    public DeleteDishCommandHandler(ILogger<CreateDishCommandHandler> logger, IRestaurantRepository restaurantRepository, IMapper mapper, IDishRepository dishRepository, IRestaurantAuthorizationService restaurantAuthorizationService)
    {
        _restaurantRepository = restaurantRepository;
        _logger = logger;
        _mapper = mapper;
        _dishRepository = dishRepository;
        this.restaurantAuthorizationService = restaurantAuthorizationService;
    }
    public async Task Handle(DeleteDishCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting Dish with Id : {dishId} for Restaurant with Id : {restaurantId}", request.dishId, request.restaurantId);

        var restaurant = await _restaurantRepository.Get(request.restaurantId);
        if (restaurant == null)
            throw new NotFoundException(nameof(Restaurant), request.restaurantId.ToString());

        if (!restaurantAuthorizationService.Authorize(restaurant, ResourceOperation.Delete))
            throw new ForbidException();

        var dish = await _dishRepository.GetById(request.restaurantId, request.dishId);
        if (dish is null)
            throw new NotFoundException(nameof(Dish), request.dishId.ToString());

        await _dishRepository.Delete(dish);
        
    }
}
