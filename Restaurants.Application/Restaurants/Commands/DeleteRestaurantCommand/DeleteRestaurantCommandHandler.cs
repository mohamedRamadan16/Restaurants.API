
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.IRepositories;

namespace Restaurants.Application.Restaurants.Commands.DeleteRestaurantCommand;

public class DeleteRestaurantCommandHandler : IRequestHandler<DeleteRestaurantCommand, bool>
{
    private readonly ILogger<DeleteRestaurantCommandHandler> _logger;
    private readonly IRestaurantRepository _restaurantRepository;
    public DeleteRestaurantCommandHandler(ILogger<DeleteRestaurantCommandHandler> logger, IRestaurantRepository repository)
    {
        _logger = logger;
        _restaurantRepository = repository;
    }
    
    public async Task<bool> Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting Restaurant With ID : {RestaurantId}", request.Id);
        var restaurant = await _restaurantRepository.Get(request.Id);
        if (restaurant is null)
            return false;
        await _restaurantRepository.Delete(restaurant);
        return true;
    }
}
