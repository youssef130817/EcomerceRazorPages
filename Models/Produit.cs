using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Ecomerce.Models
{
    public class Produit
    {
        public int Id { get; set; }
        public required string Nom { get; set; }
        public required string Description { get; set; }
        public decimal Prix { get; set; }
        public Categorie? categorie { get; set; }
        public int CategorieId { get; set; }
        public String? ImageUrl { get; set; }

        [NotMapped]
        public IFormFile? ImageFile { get; set; } // New property for the image

    }
}