

using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.IRepositories;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurantCommand;

public class CreateRestaurantCommandHandler : IRequestHandler<CreateRestaurantCommand, int>
{
    private readonly IRestaurantRepository _restaurantRepository;
    private readonly ILogger<CreateRestaurantCommandHandler> _logger;
    private readonly IMapper _mapper;

    public CreateRestaurantCommandHandler(IRestaurantRepository restaurantRepository, ILogger<CreateRestaurantCommandHandler> logger, IMapper mapper)
    {
        _restaurantRepository = restaurantRepository;
        _logger = logger;
        _mapper = mapper;
    }
    public async Task<int> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
    {
        var restaurant = _mapper.Map<Restaurant>(request);
        int id = await _restaurantRepository.CreateAsync(restaurant);
        return id;
    }
}
