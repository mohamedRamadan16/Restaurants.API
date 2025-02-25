

using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Users;
using Restaurants.Domain.Entities;
using Restaurants.Domain.IRepositories;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurantCommand;

public class CreateRestaurantCommandHandler : IRequestHandler<CreateRestaurantCommand, int>
{
    private readonly IRestaurantRepository _restaurantRepository;
    private readonly ILogger<CreateRestaurantCommandHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IUserContext _userContext;

    public CreateRestaurantCommandHandler(IRestaurantRepository restaurantRepository, ILogger<CreateRestaurantCommandHandler> logger, IMapper mapper, IUserContext userContext)
    {
        _userContext = userContext;
        _restaurantRepository = restaurantRepository;
        _logger = logger;
        _mapper = mapper;
    }
    public async Task<int> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
    {
        var currentUser = _userContext.GetCurrentUser();
        _logger.LogInformation("User {UserName} [{UserId}] is Creating New Restaurant {@Restaurant}", currentUser!.Email, currentUser.Id,request);
        var restaurant = _mapper.Map<Restaurant>(request);
        restaurant.OwnerId = currentUser.Id;

        int id = await _restaurantRepository.CreateAsync(restaurant);
        return id;
    }
}
