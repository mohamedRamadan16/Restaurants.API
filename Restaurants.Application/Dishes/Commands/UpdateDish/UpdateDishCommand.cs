using MediatR;

namespace Restaurants.Application.Dishes.Commands.UpdateDish;

public class UpdateDishCommand : IRequest
{
    public int Id { get; set; }
    public string Description { get; set; } = default!;
    public decimal Price { get; set; }
    public int? KiloCalories { get; set; }
    public int RestaurantId { get; set; }
}
