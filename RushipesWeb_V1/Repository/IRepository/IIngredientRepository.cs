using RushipesWeb_V1.Models;

namespace RushipesWeb_V1.Repository.IRepository
{
    public interface IIngredientRepository
    {
        Task<IEnumerable<Ingredient>> GetAllAsync();
        Task<Ingredient> GetAsync(int id);
        Task<Ingredient> AddAsync(Ingredient ingredient);

        Task<Ingredient?> UpdateAsync(Ingredient ingredient);
        Task<Ingredient?> DeleteAsync(int id);
    }
}
