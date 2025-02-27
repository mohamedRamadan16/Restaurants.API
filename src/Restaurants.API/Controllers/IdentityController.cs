using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Users.Commands.AssignUserRole;
using Restaurants.Application.Users.Commands.RemoveUserRole;
using Restaurants.Application.Users.Commands.UpdateUserInfo;
using Restaurants.Domain.Constants;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

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

        [HttpPost("AddUserRole")]
        [Authorize(Roles = RolesConstant.Admin)]
        public async Task<IActionResult> AssignUserRole(AssignUserRoleCommand command)
        {
            await mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("RemoveUserRole")]
        [Authorize(Roles = RolesConstant.Admin)]
        public async Task<IActionResult> RemoveUserRole(RemoveUserRoleCommand command)
        {
            await mediator.Send(command);
            return NoContent();
        }
    }
}
