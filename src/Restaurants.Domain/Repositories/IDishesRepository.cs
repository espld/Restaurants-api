using Restaurants.Domain.Entities;

namespace Restaurants.Domain.Repositories
{
    public interface IDishesRepository
    {
        Task<int> CreateAsync(Dish entity);
        Task DeleteDishesAsync(IEnumerable<Dish> entities);
    }
}
