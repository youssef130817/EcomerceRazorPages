using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Ecomerce.Data;
using Ecomerce.Models;

namespace Ecomerce.Pages_Produits
{
    public class DetailsModel : PageModel
    {
        private readonly Ecomerce.Data.ApplicationDbContext _context;

        public DetailsModel(Ecomerce.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Produit Produit { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produit = await _context.Products.FirstOrDefaultAsync(m => m.Id == id);
            if (produit == null)
            {
                return NotFound();
            }
            else
            {
                Produit = produit;
            }
            return Page();
        }
    }
}
