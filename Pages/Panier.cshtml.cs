using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Ecomerce.Models;
using Microsoft.AspNetCore.Mvc;
using Ecomerce.Services;
using System.Drawing.Printing;

namespace Ecomerce.Pages
{
    // Modèle pour gérer le panier d'achat
    public class PanierModel : PageModel
    {
        // Clé pour le cookie du panier
        private const string CookiePanierKey = "PanierCookie";
        private readonly IHttpContextAccessor _httpContextAccessor; // Accesseur HTTP
        private readonly Ecomerce.Services.PanierService _panierService; // Service panier

        // Constructeur
        public PanierModel(IHttpContextAccessor httpContextAccessor, PanierService panierService)
        {
            _httpContextAccessor = httpContextAccessor;
            _panierService = panierService;
        }

        // Propriété pour le panier
        [Microsoft.AspNetCore.Mvc.BindProperty]
        public Panier Panier { get; set; } = new Panier();

        // Méthode exécutée lors d'une requête GET
        public void OnGet()
        {
            // Charge le panier à partir du cookie
            var panierJson = Request.Cookies[CookiePanierKey];
            Panier = string.IsNullOrEmpty(panierJson) ? new Panier() : JsonConvert.DeserializeObject<Panier>(panierJson);
        }

        // Méthode pour supprimer un produit du panier
        public IActionResult OnPostSupprimerDuPanier(int productId)
        {
            var monpanier = _panierService.GetPanier(); // Récupère le panier
            var ligne = monpanier.Lignes.FirstOrDefault(l => l.ProductId == productId);
            if (ligne != null) monpanier.Lignes.Remove(ligne); // Supprime la ligne si trouvée
            SauvegarderPanier(monpanier); // Sauvegarde le panier
            return RedirectToPage(); // Redirige vers la page
        }

        // Sauvegarde le panier dans un cookie
        private void SauvegarderPanier(Panier panier)
        {
            var panierJson = JsonConvert.SerializeObject(panier); // Sérialise le panier
            var options = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(7), // Durée de vie du cookie
                HttpOnly = true, // Accès via JavaScript désactivé
                Secure = true, // Cookie sécurisé pour HTTPS
                SameSite = SameSiteMode.Lax // Politique de sécurité
            };
            Response.Cookies.Delete(CookiePanierKey); // Supprime l'ancien cookie
            Response.Cookies.Append(CookiePanierKey, panierJson, options); // Ajoute le nouveau cookie
        }

        // Méthode pour modifier la quantité d'un ligne panier
        public IActionResult OnPostModifierQuantite(int productId, int quantite)
        {
            // Charge le panier depuis le cookie
            var panierJson = Request.Cookies[CookiePanierKey];
            if (!string.IsNullOrEmpty(panierJson)) Panier = JsonConvert.DeserializeObject<Panier>(panierJson);

            var ligne = Panier.Lignes.FirstOrDefault(l => l.ProductId == productId);
            if (ligne != null) // Modifie la quantité si la ligne est trouvée
            {
                ligne.Quantite = quantite;
                SauvegarderPanier(Panier); // Sauvegarde le panier
            }

            return RedirectToPage();
        }
    }
}
