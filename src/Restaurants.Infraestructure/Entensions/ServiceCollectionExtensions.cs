using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;
using Restaurants.Infraestructure.Authorization;
using Restaurants.Infraestructure.Authorization.Requirements;
using Restaurants.Infraestructure.Authorization.Services;
using Restaurants.Infraestructure.Persistance;
using Restaurants.Infraestructure.Repositories;
using Restaurants.Infraestructure.Seeders;

namespace Restaurants.Infraestructure.Entensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfraestructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("RestaurantsDb");

            services.AddDbContext<RestaurantsDbContext>(options =>
                options.UseSqlServer(connectionString)
                       .EnableSensitiveDataLogging());

            services.AddIdentityApiEndpoints<User>()
                .AddRoles<IdentityRole>()
                .AddClaimsPrincipalFactory<RestaurantsUserClaimsPrincipalFactory>()
                .AddEntityFrameworkStores<RestaurantsDbContext>();

            services.AddScoped<IRestaurantSeeder, RestaurantSeeder>();
            services.AddScoped<IRestaurantsRepository, RestaurantsRepository>();
            services.AddScoped<IDishesRepository, DishesRepository>();
            services.AddAuthorizationBuilder()
                .AddPolicy(PolicyNames.HasNationality,
                builder => builder.RequireClaim(AppClaimsTypes.Nationality, "Argentina"))
                .AddPolicy(PolicyNames.AtLeast20,
                builder => builder.AddRequirements(new MinimunAgeRequirement(20)))
                .AddPolicy(PolicyNames.CreatedAtLeast2Restaurants,
                builder => builder.AddRequirements(new CreatedMultipleRestaurantsRequirement(2)));

            services.AddScoped<IAuthorizationHandler, MinimunAgeRequirementHandler>();
            services.AddScoped<IAuthorizationHandler, CreatedMultipleRestaurantsRequirementHandler>();
            services.AddScoped<IRestaurantAuthorizationService, RestaurantAuthorizationService>();

        }
    }

}
