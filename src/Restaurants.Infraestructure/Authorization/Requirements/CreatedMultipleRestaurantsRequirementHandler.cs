using Microsoft.AspNetCore.Authorization;
using Restaurants.Application.Users;
using Restaurants.Domain.Repositories;

namespace Restaurants.Infraestructure.Authorization.Requirements
{
    internal class CreatedMultipleRestaurantsRequirementHandler(
        IRestaurantsRepository restaurantsRepository,
        IUserContext userContext) : AuthorizationHandler<CreatedMultipleRestaurantsRequirement>
    {
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, 
            CreatedMultipleRestaurantsRequirement requirement)
        {
            var currentUser = userContext.GetCurrentUser();
            var restaurants = await restaurantsRepository.GetAllAsync();

            var userCreatedRestaurants = restaurants.Count(r => r.OwnerId == currentUser!.Id);

            if(userCreatedRestaurants >= requirement.MinimunRestaurantsCreated)
            {
                context.Succeed(requirement); 
            }
            else
            {
                context.Fail();
            }

        }
    }
}
