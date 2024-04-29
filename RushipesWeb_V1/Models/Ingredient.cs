using System.ComponentModel.DataAnnotations;

namespace RushipesWeb_V1.Models
{
    public class Ingredient
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }

        public virtual ICollection<Recette> Recettes { get; set; }
    }
}
