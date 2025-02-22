using MediatR;
using Restaurants.Application.Dishes.DTOs;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Dishes.Queries.GetDishById;

public class GetDishByIdQuery : IRequest<DishDTO>
{
    public int dishId { get; set; }
    public int restaurantId { get; set; } 
    public GetDishByIdQuery(int restaurantId,int dishId)
    {
        this.dishId = dishId;
        this.restaurantId = restaurantId;
    }
}
