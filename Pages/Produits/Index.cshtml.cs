using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Ecomerce.Data;
using Ecomerce.Models;

namespace Ecomerce.Pages_index
{
    public class IndexModel : PageModel
    {
        private readonly Ecomerce.Data.ApplicationDbContext _context;
        private readonly Ecomerce.Services.PanierService _panierService;

        public IndexModel(Ecomerce.Data.ApplicationDbContext context, Ecomerce.Services.PanierService panierService)
        {
            _context = context;
            _panierService = panierService;
        }

        public IList<Produit> Produit { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Produit = await _context.Products
                .Include(p => p.categorie)  // Assurez-vous que la propriété est bien nommée
                .ToListAsync();

            if (Produit == null || !Produit.Any())
            {
                // Logique si aucun produit n'est trouvé
                Console.WriteLine("Aucun produit trouvé.");
            }
        }

        // public IActionResult OnPostAjouterAuPanier(int productId, int quantite)
        // {
        //     // Ajouter une ligne au panier
        //     _panierService.AjouterAuPanier(productId, quantite);

        //     return RedirectToPage();
        // }


    }
}
