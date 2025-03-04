﻿using MediatR;

namespace Restaurants.Application.Users.Commands.AssignUserRole;

public class AssignUserRoleCommand : IRequest
{
    public string Email { get; set; } = default!;
    public string RoleName { get; set; } = default!;
}

