using RushipesWeb_V1.Models;

namespace RushipesWeb_V1.Repository.IRepository
{
    public interface IRecettePostRepository
    {
        Task<IEnumerable<Recette>> GetAllAsync();
        Task<Recette?> GetAsync(int id);
        Task<Recette> AddAsync(Recette recette);
        Task<Recette?> UpdateAsync(Recette recette);
        Task<Recette?> DeleteAsync(int id);
    }
}
