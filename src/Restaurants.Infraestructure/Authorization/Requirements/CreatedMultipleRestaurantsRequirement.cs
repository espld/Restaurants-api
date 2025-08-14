using Microsoft.AspNetCore.Authorization;

namespace Restaurants.Infraestructure.Authorization.Requirements
{
    public class CreatedMultipleRestaurantsRequirement(int minimunRestaurantsCreated) : IAuthorizationRequirement
    {
        public int MinimunRestaurantsCreated { get; } = minimunRestaurantsCreated;
    }
}
