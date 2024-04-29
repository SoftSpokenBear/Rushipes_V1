using Microsoft.EntityFrameworkCore;
using RushipesWeb_V1.Data;
using RushipesWeb_V1.Models;
using RushipesWeb_V1.Repository.IRepository;

namespace RushipesWeb_V1.Repository
{
    public class RecettePostRepository : IRecettePostRepository
    {
        private readonly RushipesDbContext _db;

        public RecettePostRepository(RushipesDbContext db)
        {
            _db = db;
        }
        public async Task<Recette> AddAsync(Recette recette)
        {
            await _db.Recettes.AddAsync(recette);
            await _db.SaveChangesAsync();
            return recette;
        }

        public async Task<Recette?> DeleteAsync(int id)
        {
            var recetteExistante = await _db.Recettes.FindAsync(id);
            if (recetteExistante != null)
            {
                _db.Recettes.Remove(recetteExistante);
                await _db.SaveChangesAsync();
                return recetteExistante;
            }
            return null;
        }



        public async Task<IEnumerable<Recette>> GetAllAsync()
        {
            return await _db.Recettes.Include(t => t.Ingredients).ToListAsync();
        }

        public async Task<Recette?> GetAsync(int id)
        {
            return await _db.Recettes.Include(t => t.Ingredients).FirstOrDefaultAsync(u => u.Id == id);
        }



        public async Task<Recette?> UpdateAsync(Recette recette)
        {
            var recetteExistante = await _db.Recettes.Include(t => t.Ingredients).FirstOrDefaultAsync(u => u.Id == recette.Id);

            if (recetteExistante != null)
            {
                recetteExistante.Id = recette.Id;
                recetteExistante.Titre = recette.Titre;
                recetteExistante.Description = recette.Description;
                recetteExistante.DatePubli = recette.DatePubli;
                recetteExistante.ImageUrl = recette.ImageUrl;
                recetteExistante.Auteur = recette.Auteur;
                recetteExistante.Visible = recette.Visible;
                recetteExistante.Ingredients = recette.Ingredients;
                recetteExistante.PreparationEtape = recette.PreparationEtape.ToList();
                recetteExistante.Difficulte = recette.Difficulte;

                await _db.SaveChangesAsync();
                return recetteExistante;
            }
            return null;
        }
    }
}
