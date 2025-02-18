using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Restaurants;
using Restaurants.Application.Restaurants.Commands.CreateRestaurantCommand;
using Restaurants.Application.Restaurants.DTOs;
using Restaurants.Application.Restaurants.Queries.GetAllRestaurants;
using Restaurants.Application.Restaurants.Queries.GetRestaurantById;
using Restaurants.Domain.Entities;

namespace Restaurants.API.Controllers
{
    [ApiController]
    [Route("api/restaurant")]
    public class RestaurantController:ControllerBase
    {
        private readonly IMediator _mediator;
        public RestaurantController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {

            IEnumerable<RestaurantDTO> restaurants = await _mediator.Send(new GetAllRestaurantsQuery());
            return Ok(restaurants);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0)
                return BadRequest("Enter a valid ID");
            var restaurant = await _mediator.Send(new GetRestaurantByIdQuery(id));
            if (restaurant == null)
                return NotFound();
            return Ok(restaurant);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]CreateRestaurantCommand createRestaurantCommand)
        {

            int id = await _mediator.Send(createRestaurantCommand);
            return CreatedAtAction(nameof(GetById), new { id }, null);
        }

    }
}
