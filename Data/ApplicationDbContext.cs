using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using Ecomerce.Models;

namespace Ecomerce.Data
{
    public class ApplicationDbContext : DbContext

    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Définir vos entités (tables) ici
        public DbSet<Produit> Products { get; set; }
        public DbSet<Categorie> Categories { get; set; }
    }
}