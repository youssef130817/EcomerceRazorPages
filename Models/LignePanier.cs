using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecomerce.Models
{
    [Serializable]
    public class LignePanier
    {
        public int ProductId { get; set; }
        public int Quantite { get; set; }
        public string NomProduit { get; set; }
        public decimal PrixUnitaire { get; set; }
        public decimal PrixTotal => Quantite * PrixUnitaire;
    }
}