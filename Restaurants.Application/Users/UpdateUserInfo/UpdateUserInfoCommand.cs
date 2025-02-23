using MediatR;

namespace Restaurants.Application.Users.UpdateUserInfo;

public class UpdateUserInfoCommand : IRequest
{
    public DateOnly? DateOfBirth { get; set; }
    public string? Nationality { get; set; }
}
