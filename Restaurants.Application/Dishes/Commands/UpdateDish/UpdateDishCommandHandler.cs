using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Dishes.Commands.CreateDish;
using Restaurants.Application.Dishes.DTOs;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.IRepositories;

namespace Restaurants.Application.Dishes.Commands.UpdateDish;

public class UpdateDishCommandHandler : IRequestHandler<UpdateDishCommand>
{
    private readonly ILogger<CreateDishCommandHandler> _logger;
    private readonly IRestaurantRepository _restaurantRepository;
    private readonly IMapper _mapper;
    private readonly IDishRepository _dishRepository;
    public UpdateDishCommandHandler(ILogger<CreateDishCommandHandler> logger, IRestaurantRepository restaurantRepository, IMapper mapper, IDishRepository dishRepository)
    {
        _restaurantRepository = restaurantRepository;
        _logger = logger;
        _mapper = mapper;
        _dishRepository = dishRepository;
    }

    public async Task Handle(UpdateDishCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating Dish with Id : {dishId} for Restaurant with Id : {restaurantId}", request.Id, request.RestaurantId);

        var restaurant = await _restaurantRepository.Get(request.RestaurantId);
        if (restaurant == null)
            throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

        var dish = await _dishRepository.GetById(request.RestaurantId, request.Id);
        if (dish is null)
            throw new NotFoundException(nameof(Dish), request.Id.ToString());

        _mapper.Map(request, dish);
        await _dishRepository.SaveChanges();
    }
}
