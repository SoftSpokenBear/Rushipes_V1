using Microsoft.AspNetCore.Mvc;
using RushipesWeb_V1.Data;
using RushipesWeb_V1.Models;
using System.Diagnostics;

namespace RushipesWeb_V1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly RushipesDbContext _db;

        public HomeController(ILogger<HomeController> logger, RushipesDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            List<Recette> listeRecette = _db.Recettes.ToList();
            return View(listeRecette);
        }

        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
