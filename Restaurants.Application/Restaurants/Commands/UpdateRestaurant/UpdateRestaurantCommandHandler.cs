
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.IRepositories;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant;

public class UpdateRestaurantCommandHandler : IRequestHandler<UpdateRestaurantCommand, bool>
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
    public async Task<bool> Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Updating Entity With Id {request.Id}");
        var restaurant = await _restaurantRepository.Get(request.Id);
        if (restaurant == null)
            return false;

        _mapper.Map(request, restaurant);

        await _restaurantRepository.SaveChanges();
        return true;
    }
}
