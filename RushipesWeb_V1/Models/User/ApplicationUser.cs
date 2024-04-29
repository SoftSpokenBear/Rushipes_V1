using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace RushipesWeb_V1.Models.User
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string Nom { get; set; }
        [Required]
        public string Prenom { get; set; }
        public string? Adresse { get; set; }
        public string? Ville { get; set; }
        public string? CodePostale { get; set; }

    }
}
