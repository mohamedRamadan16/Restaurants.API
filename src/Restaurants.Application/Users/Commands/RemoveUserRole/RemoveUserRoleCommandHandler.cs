using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;

namespace Restaurants.Application.Users.Commands.RemoveUserRole;

public class RemoveUserRoleCommandHandler(ILogger<RemoveUserRoleCommandHandler> logger,
    UserManager<User> userManager,
    RoleManager<IdentityRole> roleManager) : IRequestHandler<RemoveUserRoleCommand>
{
    public async Task Handle(RemoveUserRoleCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Removing Role {role} for the user with email {email}", request.RoleName, request.Email);

        var user = await userManager.FindByEmailAsync(request.Email) ?? throw new NotFoundException(nameof(User), request.Email);
        var role = await roleManager.FindByNameAsync(request.RoleName) ?? throw new NotFoundException(nameof(IdentityRole), request.RoleName);

        await userManager.RemoveFromRoleAsync(user, role.Name!);
    }
}
