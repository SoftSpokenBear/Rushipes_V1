using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RushipesWeb_V1.Models.ViewModels
{
    public class AddRecetteRequest
    {
        [Required(ErrorMessage = "Le Titre est Obligatoire")]
        [MaxLength(50)]
        public string Titre { get; set; } = string.Empty;
        public string? Description { get; set; }
        [ValidateNever]
        public List<string> PreparationEtape { get; set; }

        public string PreparationEtapeJson { get; set; } = string.Empty;
        [NotMapped]
        public IFormFile? Image { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime DatePubli { get; set; } = DateTime.Now;

        public string Difficulte { get; set; } = string.Empty;

        public string Auteur { get; set; } = string.Empty;
        public bool Visible { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> Ingrediens { get; set; }
        [ValidateNever]
        public string[] SlectedIngredients { get; set; } = Array.Empty<string>();
    }
}
