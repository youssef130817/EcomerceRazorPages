using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecomerce.Models
{
    public class Categorie
    {
        public int Id { get; set; }
        public required string Nom { get; set; }

        public ICollection<Produit> Produits { get; set; } = [];
    }
}