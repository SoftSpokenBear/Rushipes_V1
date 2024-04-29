using Microsoft.EntityFrameworkCore;
using RushipesWeb_V1.Data;
using RushipesWeb_V1.Models;
using RushipesWeb_V1.Repository.IRepository;

namespace RushipesWeb_V1.Repository
{
    public class IngredientRepository : IIngredientRepository
    {
        private readonly RushipesDbContext _db;

        public IngredientRepository(RushipesDbContext db)
        {
            _db = db;
        }


        public async Task<Ingredient> AddAsync(Ingredient ingredient)
        {

            await _db.Ingredients.AddAsync(ingredient);
            await _db.SaveChangesAsync();


            return ingredient;
        }

        public async Task<Ingredient?> DeleteAsync(int id)
        {

            var existingIngredient = await _db.Ingredients.FindAsync(id);

            if (existingIngredient != null)
            {

                _db.Ingredients.Remove(existingIngredient);


                await _db.SaveChangesAsync();

                return existingIngredient;
            }
            return null;
        }


        public async Task<IEnumerable<Ingredient>> GetAllAsync()
        {
            return await _db.Ingredients.ToListAsync();
        }

        public Task<Ingredient?> GetAsync(int id)
        {
            return _db.Ingredients.FirstOrDefaultAsync(x => x.Id == id);
        }



        public async Task<Ingredient?> UpdateAsync(Ingredient ingredient)
        {

            var existingIngredient = await _db.Ingredients.FindAsync(ingredient.Id);

            if (existingIngredient != null)
            {
                existingIngredient.Name = ingredient.Name;


                await _db.SaveChangesAsync();

                return existingIngredient;
            }

            return null;
        }
    }
}
