using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RushipesWeb_V1.Models;
using RushipesWeb_V1.Models.User;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace RushipesWeb_V1.Data
{
    public class RushipesDbContext : IdentityDbContext<IdentityUser>
    {
        public virtual DbSet<Ingredient> Ingredients { get; set; }
        public virtual DbSet<Recette> Recettes { get; set; }
        public virtual DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public RushipesDbContext(DbContextOptions<RushipesDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
