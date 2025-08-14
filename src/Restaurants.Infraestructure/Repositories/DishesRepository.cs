using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infraestructure.Persistance;

namespace Restaurants.Infraestructure.Repositories
{
    internal class DishesRepository(RestaurantsDbContext dbContext) : IDishesRepository
    {
        public async Task<int> CreateAsync(Dish entity)
        {
            dbContext.Dishes.Add(entity);
            await dbContext.SaveChangesAsync();
            return entity.Id;
        }

        public async Task DeleteDishesAsync(IEnumerable<Dish> entities)
        {
            dbContext.Dishes.RemoveRange(entities);
            await dbContext.SaveChangesAsync();
        }
    }
}
