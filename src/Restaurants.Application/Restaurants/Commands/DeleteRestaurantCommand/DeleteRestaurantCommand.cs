
using MediatR;

namespace Restaurants.Application.Restaurants.Commands.DeleteRestaurantCommand;

public class DeleteRestaurantCommand : IRequest
{
    public int Id { get; set; }
    public DeleteRestaurantCommand(int id)
    {
        Id = id;
    }
}
