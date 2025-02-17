
using System.ComponentModel.DataAnnotations;

namespace Restaurants.Application.Restaurants.DTOs
{
    public class CreateRestaurantDTO
    {
        [StringLength(100, MinimumLength = 4)]
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        [Required(ErrorMessage = "Add A Category")]
        public string Category { get; set; } = default!;
        public bool HasDelivery { get; set; }
        [EmailAddress(ErrorMessage = "Enter a valid Email")]
        public string? ContactEmail { get; set; }
        [Phone(ErrorMessage = "Enter a valid phone number")]
        public string? ContactNumber { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; }
        [RegularExpression(@"^\d{2}-\d{3}$", ErrorMessage = "Please Provide a postal code (XX-XXX)")]
        public string? PostalCode { get; set; }
    }
}
