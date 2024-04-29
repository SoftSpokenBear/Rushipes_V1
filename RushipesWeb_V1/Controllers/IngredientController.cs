using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using RushipesWeb_V1.Models.ViewModels;
using RushipesWeb_V1.Models;
using RushipesWeb_V1.Repository.IRepository;

namespace RushipesWeb_V1.Controllers
{
    public class IngredientController : Controller
    {
        private readonly IIngredientRepository _ingredientRepository;

        public IngredientController(IIngredientRepository ingredientRepository)
        {
            _ingredientRepository = ingredientRepository;
        }
        public async Task<IActionResult> Index()
        {
            var listeIngredient = await _ingredientRepository.GetAllAsync();
            return View(listeIngredient);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddIngredientRequest addIngredientRequest)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var ingredient = new Ingredient
            {
                Name = addIngredientRequest.Name,
            };
            await _ingredientRepository.AddAsync(ingredient);

            return RedirectToAction(nameof(Index));


        }

        public async Task<IActionResult> Edit(int id)
        {


            var ingredient = await _ingredientRepository.GetAsync(id);

            if (ingredient != null)
            {
                var editIngredientRequest = new EditIngredientRequest
                {
                    Id = ingredient.Id,
                    Name = ingredient.Name,

                };
                return View(editIngredientRequest);
            }
            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditIngredientRequest editIngredientRequest)
        {
            var ingredient = new Ingredient
            {
                Id = editIngredientRequest.Id,
                Name = editIngredientRequest.Name,

            };

            var updatedIngredient = await _ingredientRepository.UpdateAsync(ingredient);



            return RedirectToAction("Index", new { id = editIngredientRequest.Id });
        }



        [HttpPost]
        public async Task<IActionResult> Delete(EditIngredientRequest editIngredientRequest)
        {
            var deletedIngredient = await _ingredientRepository.DeleteAsync(editIngredientRequest.Id);

            if (deletedIngredient != null)
            {

                return RedirectToAction(nameof(Index));
            }


            return RedirectToAction("Index", new { id = editIngredientRequest.Id });
        }
    }
}
