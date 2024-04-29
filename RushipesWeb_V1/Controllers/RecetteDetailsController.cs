using Microsoft.AspNetCore.Mvc;
using RushipesWeb_V1.Repository.IRepository;

namespace RushipesWeb_V1.Controllers
{
    public class RecetteDetailsController : Controller
    {
        private readonly IRecettePostRepository _recettePostRepository;
        public RecetteDetailsController(
            IRecettePostRepository recettePostRepository)
        {
            _recettePostRepository = recettePostRepository;

        }

        public async Task<IActionResult> Details(int recetteId)
        {
            var recette = await _recettePostRepository.GetAsync(recetteId);


            return View(recette);
        }
    }
}
