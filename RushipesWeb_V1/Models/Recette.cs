using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RushipesWeb_V1.Models
{
    public class Recette
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Le Titre est Obligatoire")]
        [MaxLength(50)]
        public string Titre { get; set; } = string.Empty;
        public string? Description { get; set; }
        [Required(ErrorMessage = "Les étapes de préparation sont requises")]
        public List<string> PreparationEtape { get; set; }
        [NotMapped]
        public IFormFile? Image { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime DatePubli { get; set; }
        public string Auteur { get; set; } = string.Empty;
        public bool Visible { get; set; }

        [Required(ErrorMessage = "La difficulté est requise ")]
        public string Difficulte { get; set; } = string.Empty;

        public ICollection<Ingredient> Ingredients { get; set; }

        public Recette()
        {
            PreparationEtape = new List<string>();
        }
    }
}
