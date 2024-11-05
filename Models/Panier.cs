using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecomerce.Models
{
    [Serializable]
    public class Panier
    {
        public List<LignePanier> Lignes { get; set; } = new List<LignePanier>();

        // Ajouter une ligne au panier
        public void AjouterLigne(int productId, string NomProduit, decimal prixUnitaire, int quantite)
        {
            var ligne = Lignes.FirstOrDefault(l => l.ProductId == productId);
            if (ligne != null)
            {
                ligne.Quantite += quantite;
            }
            else
            {
                Lignes.Add(new LignePanier
                {
                    ProductId = productId,
                    Quantite = quantite,
                    NomProduit = NomProduit,
                    PrixUnitaire = prixUnitaire
                });
            }
        }
        // Supprimer une ligne du panier
        public void SupprimerLigne(int productId)
        {
            var ligne = Lignes.FirstOrDefault(l => l.ProductId == productId);
            if (ligne != null)
            {
                Lignes.Remove(ligne);
            
            }
        }

        // Calculer le total du panier
        public decimal Total()
        {
            return Lignes.Sum(l => l.PrixTotal);
        }
        public void ModifierQuantite(int productId, int nouvelleQuantite)
        {
            var ligne = Lignes.FirstOrDefault(l => l.ProductId == productId);
            if (ligne != null)
            {
                ligne.Quantite = nouvelleQuantite;
            }
        }
    }
}