using Microsoft.AspNetCore.Authorization;

namespace Restaurants.Infraestructure.Authorization.Requirements
{
    public class MinimunAgeRequirement(int minimunAge) : IAuthorizationRequirement
    {
        public int MinimumAge { get; } = minimunAge;
    }
}
