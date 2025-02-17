

using Restaurants.Application.Dishes.DTOs;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Restaurants.DTOs
{
    public class RestaurantDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Category { get; set; } = default!;
        public bool HasDelivery { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; }
        public string? PostalCode { get; set; }
        public List<DishDTO> Dishes { get; set; } = [];
        public static RestaurantDTO? FromEntity(Restaurant? restaurant)
        {
            if (restaurant == null)
                return null;

            return new RestaurantDTO()
            {
                Id = restaurant.Id,
                Name = restaurant.Name,
                Description = restaurant.Description,
                Category = restaurant.Category,
                HasDelivery = restaurant.HasDelivery,
                City = restaurant.Address?.City,
                Street = restaurant.Address?.Street,
                PostalCode = restaurant.Address?.PostalCode,
                Dishes = restaurant.Dishes.Select(DishDTO.FromEntity).ToList()
            };
        }
    }
}
