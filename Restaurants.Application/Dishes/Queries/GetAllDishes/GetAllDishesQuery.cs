using MediatR;
using Restaurants.Application.Dishes.DTOs;

namespace Restaurants.Application.Dishes.Queries.GetAllDishes;

public class GetAllDishesQuery : IRequest<IEnumerable<DishDTO>>
{
    public int RestaurantId { get; set; }
    public GetAllDishesQuery(int restaurantId)
    {
        RestaurantId = restaurantId;
    }

}
