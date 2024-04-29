using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;

namespace RushipesWeb_V1.Models.ViewModels
{
    public class RecetteDetailsViewModel
    {
        public string? Titre { get; set; }
        public string? Description { get; set; }
        public IEnumerable<string> PreparationEtape { get; set; } = Enumerable.Empty<string>();
        [NotMapped]
        public IFormFile? Image { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime DatePubli { get; set; }
        public string? Difficulte { get; set; }
        public string? Auteur { get; set; }
        public bool Visible { get; set; }

        public IEnumerable<SelectListItem> Ingrediens { get; set; } = Enumerable.Empty<SelectListItem>();
        public string[] SlectedIngredients { get; set; } = Array.Empty<string>();
    }
}
