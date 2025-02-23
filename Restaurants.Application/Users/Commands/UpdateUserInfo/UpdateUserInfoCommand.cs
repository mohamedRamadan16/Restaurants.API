using MediatR;

namespace Restaurants.Application.Users.Commands.UpdateUserInfo;

public class UpdateUserInfoCommand : IRequest
{
    public DateOnly? DateOfBirth { get; set; }
    public string? Nationality { get; set; }
}
