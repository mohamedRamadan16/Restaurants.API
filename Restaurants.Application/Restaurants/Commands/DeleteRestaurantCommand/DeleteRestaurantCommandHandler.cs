
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.IRepositories;

namespace Restaurants.Application.Restaurants.Commands.DeleteRestaurantCommand;

public class DeleteRestaurantCommandHandler : IRequestHandler<DeleteRestaurantCommand>
{
    private readonly ILogger<DeleteRestaurantCommandHandler> _logger;
    private readonly IRestaurantRepository _restaurantRepository;
    private readonly IRestaurantAuthorizationService restaurantAuthorizationService;

    public DeleteRestaurantCommandHandler(ILogger<DeleteRestaurantCommandHandler> logger, IRestaurantRepository repository, IRestaurantAuthorizationService restaurantAuthorizationService)
    {
        _logger = logger;
        _restaurantRepository = repository;
        this.restaurantAuthorizationService = restaurantAuthorizationService;
    }
    
    public async Task Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting Restaurant With ID : {RestaurantId}", request.Id);

        var restaurant = await _restaurantRepository.Get(request.Id);
        if (restaurant is null)
            throw new NotFoundException(nameof(Restaurant), request.Id.ToString());

        if (!restaurantAuthorizationService.Authorize(restaurant, ResourceOperation.Delete))
            throw new ForbidException();

        await _restaurantRepository.Delete(restaurant);
    }
}
