using Ecomerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace Ecomerce.Pages_Produits
{

    public class ProduitsModel : PageModel
    {
        public List<Produit>? produits { get; set; }
        private readonly Ecomerce.Data.ApplicationDbContext _context;
        private readonly Ecomerce.Services.PanierService _panierService;
        public List<Produit> ProduitsFiltrés { get; set; }

        [BindProperty(SupportsGet = true)]
        public string CategorieRecherche { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        public ProduitsModel(Ecomerce.Data.ApplicationDbContext context, Ecomerce.Services.PanierService panierService)
        {
            _context = context;
            _panierService = panierService;
            ProduitsFiltrés = new List<Produit>();
            CategorieRecherche = string.Empty;
            SearchTerm = string.Empty;

        }

        public async Task OnGetAsync()
        {
            // Populate the category dropdown list
            ViewData["CategorieId"] = new SelectList(_context.Categories, "Id", "Nom");

            // Get all products
            var produits = await GetAllProduits();

            // Apply category filter if CategorieRecherche is not null or empty
            if (!string.IsNullOrWhiteSpace(CategorieRecherche) && int.TryParse(CategorieRecherche, out int categorieId) && categorieId != 0)
            {
                produits = produits
                    .Where(p => p.categorie != null && p.categorie.Id == categorieId)
                    .ToList();
            }

            // Apply search term filter if SearchTerm is not null or empty
            if (!string.IsNullOrWhiteSpace(SearchTerm))
            {
                produits = produits
                    .Where(p => p.Nom.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            // Set the filtered products
            ProduitsFiltrés = produits;
        }


        public IActionResult OnPostAjouterAuPanier(int productId, string nom, decimal prixUnitaire, int quantite)
        {
            // Ajouter une ligne au panier
            _panierService.AjouterAuPanier(productId, nom, prixUnitaire, quantite);

            return RedirectToPage();
        }

        private async Task<List<Produit>> GetAllProduits()
        {
            // Obtenir les produits depuis la base de données avec leurs catégories
            return await _context.Products
                .Include(p => p.categorie)
                .ToListAsync();
        }
    }
}
