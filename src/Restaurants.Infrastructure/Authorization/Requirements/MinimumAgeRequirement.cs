﻿using Microsoft.AspNetCore.Authorization;

namespace Restaurants.Infrastructure.Authorization.Requirements;

public class MinimumAgeRequirement(int minAge) : IAuthorizationRequirement
{
    public int MinAge { get; set; } = minAge;
}
