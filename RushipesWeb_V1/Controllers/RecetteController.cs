using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using RushipesWeb_V1.Models.ViewModels;
using RushipesWeb_V1.Models;
using RushipesWeb_V1.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;

namespace RushipesWeb_V1.Controllers
{
    [Authorize]
    public class RecetteController : Controller
    {
        private readonly IIngredientRepository _ingredientRepository;
        private readonly IRecettePostRepository _recettePostRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;


        public RecetteController(IIngredientRepository ingredientRepository
            , IRecettePostRepository recettePostRepository
            , IWebHostEnvironment webHostEnvironment)
        {
            _recettePostRepository = recettePostRepository;
            _ingredientRepository = ingredientRepository;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<IActionResult> Index()
        {
            var recettes = await _recettePostRepository.GetAllAsync();
            return View(recettes);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var ingredients = await _ingredientRepository.GetAllAsync();
            var model = new AddRecetteRequest
            {
                Ingrediens = ingredients.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() })
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddRecetteRequest addRecetteRequest)
        {
            if (ModelState.IsValid)
            {
                var imageRecette = addRecetteRequest.ImageUrl;

                if (addRecetteRequest.Image != null)
                {
                    string filename = Guid.NewGuid().ToString() + Path.GetExtension(addRecetteRequest.Image.FileName);
                    string cheminImage = Path.Combine(_webHostEnvironment.WebRootPath, @"images\RecetteImage");

                    using var fileStream = new FileStream(Path.Combine(cheminImage, filename), FileMode.Create);

                    addRecetteRequest.Image.CopyTo(fileStream);
                    imageRecette = @"\images\RecetteImage\" + filename;
                }
                else
                {
                    imageRecette = @"\images\PlaceHold\placehold1.png";
                }

                var preparationEtapeList = JsonConvert.DeserializeObject<List<string>>(addRecetteRequest.PreparationEtapeJson);

                var recette = new Recette
                {
                    Titre = addRecetteRequest.Titre,
                    Description = addRecetteRequest.Description,
                    PreparationEtape = preparationEtapeList,
                    ImageUrl = imageRecette,
                    DatePubli = addRecetteRequest.DatePubli,
                    Auteur = addRecetteRequest.Auteur,
                    Visible = addRecetteRequest.Visible,
                    Difficulte = addRecetteRequest.Difficulte,
                };




                var selectedIngredients = new List<Ingredient>();
                foreach (var selectedIngredientId in addRecetteRequest.SlectedIngredients)
                {
                    var selectedIngredientInt = int.Parse(selectedIngredientId);
                    var existingIngredient = await _ingredientRepository.GetAsync(selectedIngredientInt);

                    if (existingIngredient != null)
                    {
                        selectedIngredients.Add(existingIngredient);
                    }
                }

                recette.Ingredients = selectedIngredients;

                await _recettePostRepository.AddAsync(recette);
                return RedirectToAction(nameof(Index));
            }



            var ingredients = await _ingredientRepository.GetAllAsync();
            addRecetteRequest.Ingrediens = ingredients.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() });
            return View(addRecetteRequest);
        }

        public async Task<IActionResult> Edit(int id)
        {


            var recette = await _recettePostRepository.GetAsync(id);
            var tags = await _ingredientRepository.GetAllAsync();

            if (recette != null)
            {
                EditRecetteRequest model = new()
                {
                    Id = recette.Id,
                    Titre = recette.Titre,
                    Description = recette.Description,
                    PreparationEtape = recette.PreparationEtape.ToList(),
                    ImageUrl = recette.ImageUrl,
                    DatePubli = recette.DatePubli,
                    Auteur = recette.Auteur,
                    Visible = recette.Visible,
                    Difficulte = recette.Difficulte,
                    Ingrediens = tags.Select(x => new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Id.ToString()
                    }),
                    SlectedIngredients = recette.Ingredients.Select(x => x.Id.ToString()).ToArray()
                };
                return View(model);
            }
            return View(null);




        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditRecetteRequest editRecetteRequest)
        {

            if (ModelState.IsValid)
            {
                var imageRecette = editRecetteRequest.ImageUrl;

                if (editRecetteRequest.Image != null)
                {
                    string filename = Guid.NewGuid().ToString() + Path.GetExtension(editRecetteRequest.Image.FileName);
                    string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, @"images\RecetteImage");

                    if (!string.IsNullOrEmpty(editRecetteRequest.ImageUrl))
                    {
                        var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, editRecetteRequest.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using var fileSteam = new FileStream(Path.Combine(imagePath, filename), FileMode.Create);

                    editRecetteRequest.Image.CopyTo(fileSteam);
                    imageRecette = @"\images\RecetteImage\" + filename;
                }



                var preparationEtapeList = JsonConvert.DeserializeObject<List<string>>(editRecetteRequest.PreparationEtapeJson);


                var recette = new Recette
                {
                    Id = editRecetteRequest.Id,
                    Titre = editRecetteRequest.Titre,
                    Description = editRecetteRequest.Description,
                    PreparationEtape = preparationEtapeList,
                    ImageUrl = imageRecette,
                    Visible = editRecetteRequest.Visible,
                    Auteur = editRecetteRequest.Auteur,
                    DatePubli = editRecetteRequest.DatePubli,
                    Difficulte = editRecetteRequest.Difficulte,

                };


                var selectedIngredients = new List<Ingredient>();

                foreach (var selectedIngredient in editRecetteRequest.SlectedIngredients)
                {
                    if (int.TryParse(selectedIngredient, out var ingredient))
                    {
                        var foundTag = await _ingredientRepository.GetAsync(ingredient);

                        if (foundTag != null)
                        {
                            selectedIngredients.Add(foundTag);
                        }
                    }
                }
                recette.Ingredients = selectedIngredients;
                var majRecette = await _recettePostRepository.UpdateAsync(recette);
                return RedirectToAction("Index");
            }

            var ingredients = await _ingredientRepository.GetAllAsync();
            editRecetteRequest.Ingrediens = ingredients.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() });
            return View(editRecetteRequest);
        }






        [HttpPost]
        public async Task<IActionResult> Delete(EditRecetteRequest editRecetteRequest)
        {

            var suppRecetteImage = await _recettePostRepository.GetAsync(editRecetteRequest.Id);

            if (suppRecetteImage != null)
            {
                if (!string.IsNullOrEmpty(suppRecetteImage.ImageUrl))
                {
                    var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, suppRecetteImage.ImageUrl.TrimStart('\\'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

            }
            var recetteSupprime = await _recettePostRepository.DeleteAsync(editRecetteRequest.Id);

            if (recetteSupprime != null)
            {
                return RedirectToAction(nameof(Index));
            }
            return NotFound();


        }
    }
}
