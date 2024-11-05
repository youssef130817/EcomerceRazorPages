using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecomerce.Models;
using Newtonsoft.Json;

namespace Ecomerce.Services
{
    public class PanierService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private const string CookiePanierKey = "PanierCookie";

        public PanierService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        // Lire le panier à partir des cookies
        public Panier GetPanier()
        {
            var panierJson = _httpContextAccessor.HttpContext?.Request?.Cookies[CookiePanierKey];
            if (string.IsNullOrEmpty(panierJson))
            {
                return new Panier();
            }

            var panier = JsonConvert.DeserializeObject<Panier>(panierJson);
            return panier ?? new Panier();
        }

        // Sauvegarder le panier dans les cookies
        public void SauvegarderPanier(Panier panier)
        {
            var panierJson = JsonConvert.SerializeObject(panier);
            var options = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(7),  // Le cookie dure 7 jours
            };

            if (_httpContextAccessor.HttpContext != null)
            {
                _httpContextAccessor.HttpContext.Response.Cookies.Append(CookiePanierKey, panierJson, options);
            }
        }

        // Ajouter une ligne au panier
        public void AjouterAuPanier(int productId, string nom, decimal prixUnitaire, int quantite)
        {
            var panier = GetPanier();
            panier.AjouterLigne(productId, nom, prixUnitaire, quantite);
            SauvegarderPanier(panier);
        }

        // Supprimer une ligne du panier
        public void SupprimerLigne(int productId)
        {
            var panier = GetPanier();
            
            panier.SupprimerLigne(productId);
            SauvegarderPanier(panier);
        }

    }
}