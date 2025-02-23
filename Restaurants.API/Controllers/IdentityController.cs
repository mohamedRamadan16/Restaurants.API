using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Users.UpdateUserInfo;

namespace Restaurants.API.Controllers
{
    [ApiController]
    [Route("api/identity")]
    [Authorize]
    public class IdentityController(IMediator mediator) : ControllerBase
    {
        [HttpPatch("user")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateUserInfo(UpdateUserInfoCommand updateUserInfoQuery)
        {
            await mediator.Send(updateUserInfoQuery);
            return NoContent();
        }
    }
}
