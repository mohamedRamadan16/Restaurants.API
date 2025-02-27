using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Users;
using Restaurants.Domain.IRepositories;

namespace Restaurants.Infrastructure.Authorization.Requirements;
internal class OwnTwoRestaurantsRequirementHandler(IUserContext userContext,
    IRestaurantRepository restaurantRepository) : AuthorizationHandler<OwnTwoRestaurantsRequirement>
{
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, OwnTwoRestaurantsRequirement requirement)
    {
        var user = userContext.GetCurrentUser();

        var restaurants = await restaurantRepository.GetAll();
        var ownedRestaurants = restaurants.Count(r => r.OwnerId == user!.Id);
        if (ownedRestaurants >= requirement.NumOfRestaurantsOwned)
            context.Succeed(requirement);
        else
            context.Fail();

    }
}
