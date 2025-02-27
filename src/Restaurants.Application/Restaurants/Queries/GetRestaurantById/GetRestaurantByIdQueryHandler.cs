
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.DTOs;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.IRepositories;

namespace Restaurants.Application.Restaurants.Queries.GetRestaurantById;

public class GetRestaurantByIdQueryHandler : IRequestHandler<GetRestaurantByIdQuery, RestaurantDTO>
{
    private readonly IRestaurantRepository _restaurantRepository;
    private readonly ILogger<GetRestaurantByIdQueryHandler> _logger;
    private readonly IMapper _mapper;

    public GetRestaurantByIdQueryHandler(IRestaurantRepository restaurantRepository, ILogger<GetRestaurantByIdQueryHandler> logger, IMapper mapper)
    {
        _restaurantRepository = restaurantRepository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<RestaurantDTO> Handle(GetRestaurantByIdQuery request, CancellationToken cancellationToken)
    {
        if (request.Id <= 0)
            throw new NotFoundException(nameof(Restaurant), request.Id.ToString());

        _logger.LogInformation("Getting Restaurant with id = {RestaurantId}", request.Id);
        var restaurant = await _restaurantRepository.Get(request.Id) ?? throw new NotFoundException(nameof(Restaurant), request.Id.ToString());
        var restaurantDTO = _mapper.Map<RestaurantDTO>(restaurant);
        return restaurantDTO;
    }
}
