using Microsoft.AspNetCore.Authorization;

namespace Restaurants.Infrastructure.Authorization.Requirements;

public class OwnTwoRestaurantsRequirement(int minRestaurentOwned) : IAuthorizationRequirement
{
    public int NumOfRestaurantsOwned { get; set; } = minRestaurentOwned;
}
