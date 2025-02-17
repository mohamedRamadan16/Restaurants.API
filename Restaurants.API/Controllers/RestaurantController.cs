using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Restaurants;
using Restaurants.Application.Restaurants.DTOs;
using Restaurants.Domain.Entities;

namespace Restaurants.API.Controllers
{
    [Controller]
    [Route("api/restaurant")]
    public class RestaurantController:ControllerBase
    {
        private readonly IRestaurantService _restaurantService;
        public RestaurantController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            
            IEnumerable<RestaurantDTO> restaurants = await _restaurantService.GetAll();
            return Ok(restaurants);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id <= 0)
                return BadRequest("Enter a valid ID");
            var restaurant = await _restaurantService.Get(id);
            if (restaurant == null)
                return NotFound();
            return Ok(restaurant);
        }
    }
}
