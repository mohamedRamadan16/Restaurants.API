using MediatR;
using Restaurants.Application.Common;
using Restaurants.Application.Restaurants.DTOs;
using Restaurants.Domain.Constants;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurants;
public class GetAllRestaurantsQuery : IRequest<PagedResults<RestaurantDTO>>
{
    public string? searchQuery { get; set; }
    public int pageNumber { get; set; }
    public int pageSize { get; set; }
    public string? SortBy { get; set; }
    public SortOption SortOption { get; set; }
}
