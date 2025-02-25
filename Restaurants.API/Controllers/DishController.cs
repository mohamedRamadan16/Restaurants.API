using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Dishes.Commands.CreateDish;
using Restaurants.Application.Dishes.Commands.DeleteDish;
using Restaurants.Application.Dishes.Commands.UpdateDish;
using Restaurants.Application.Dishes.DTOs;
using Restaurants.Application.Dishes.Queries.GetAllDishes;
using Restaurants.Application.Dishes.Queries.GetDishById;
using Restaurants.Infrastructure.Authorization;

namespace Restaurants.API.Controllers
{
    [ApiController]
    [Route("api/restaurant/{restaurantId}/dishes")]
    public class DishController : ControllerBase
    {
        private readonly IMediator _mediator;
        public DishController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DishDTO>>> GetAll([FromRoute] int restaurantId)
        {
            var DishList = await _mediator.Send(new GetAllDishesQuery(restaurantId));
            return Ok(DishList);
        }

        [HttpGet("{dishId:int}")]
        [Authorize(Policy = PolicyNames.AtLeast20)]
        public async Task<ActionResult<DishDTO>> GetById([FromRoute] int restaurantId, [FromRoute] int dishId)
        {

            var dishDto = await _mediator.Send(new GetDishByIdQuery(restaurantId, dishId));
            return Ok(dishDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromRoute]int restaurantId, CreateDishCommand createDishCommand)
        {
            createDishCommand.RestaurantId = restaurantId;
            int Dishid = await _mediator.Send(createDishCommand);
            return CreatedAtAction(nameof(GetById), new { restaurantId, Dishid }, null);
        }

        [HttpPatch("{dishId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromRoute] int restaurantId, [FromRoute] int dishId, [FromBody] UpdateDishCommand updateDishCommand)
        {
            updateDishCommand.Id = dishId;
            updateDishCommand.RestaurantId = restaurantId;
            await _mediator.Send(updateDishCommand);
            return NoContent();
        }

        [HttpDelete("{dishId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([FromRoute] int restaurantId, [FromRoute] int dishId)
        {
            await _mediator.Send(new DeleteDishCommand(restaurantId, dishId));
            return NoContent();
        }

    }
}
