using MediatR;

namespace Restaurants.Application.Dishes.Commands.DeleteDish;

public class DeleteDishCommand : IRequest
{
    public int dishId { get; set; }
    public int restaurantId { get; set; }
    public DeleteDishCommand(int restaurantId, int dishId)
    {
        this.restaurantId = restaurantId;
        this.dishId = dishId;
    }
}
