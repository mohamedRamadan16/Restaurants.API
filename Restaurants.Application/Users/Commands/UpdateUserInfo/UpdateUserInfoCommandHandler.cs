using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;

namespace Restaurants.Application.Users.Commands.UpdateUserInfo;

public class UpdateUserInfoCommandHandler(ILogger<UpdateUserInfoCommandHandler> logger,
    IUserContext userContext,
    IUserStore<User> userStore) : IRequestHandler<UpdateUserInfoCommand>
{
    public async Task Handle(UpdateUserInfoCommand request, CancellationToken cancellationToken)
    {
        var user = userContext.GetCurrentUser();
        logger.LogInformation("Updating User With Id {UserId} By Values {@UpdatedUser}", user!.Id, request);

        // Getting User From Database
        var userFromDb = await userStore.FindByIdAsync(user.Id, cancellationToken);
        if (userFromDb == null)
            throw new NotFoundException(nameof(User), user!.Id);

        userFromDb.DateOfBirth = request.DateOfBirth;
        userFromDb.Nationality = request.Nationality;

        await userStore.UpdateAsync(userFromDb, cancellationToken);
    }
}
