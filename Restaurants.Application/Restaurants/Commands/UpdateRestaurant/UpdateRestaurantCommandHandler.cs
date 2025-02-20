
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.IRepositories;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant;

public class UpdateRestaurantCommandHandler : IRequestHandler<UpdateRestaurantCommand>
{
    private readonly IRestaurantRepository _restaurantRepository;
    private readonly ILogger<UpdateRestaurantCommandHandler> _logger;
    private readonly IMapper _mapper;

    public UpdateRestaurantCommandHandler(IRestaurantRepository restaurantRepository, ILogger<UpdateRestaurantCommandHandler> logger, IMapper mapper)
    {
        _logger = logger;
        _mapper = mapper;
        _restaurantRepository = restaurantRepository;
    }
    public async Task Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating restaurant with id: {RestaurantId} with {@UpdatedRestaurant}", request.Id, request);
        var restaurant = await _restaurantRepository.Get(request.Id);
        if (restaurant == null)
            throw new NotFoundException(nameof(Restaurant), request.Id.ToString());

        _mapper.Map(request, restaurant);
        await _restaurantRepository.SaveChanges();
    }
}
