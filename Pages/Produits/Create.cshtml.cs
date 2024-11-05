using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Ecomerce.Data;
using Ecomerce.Models;

namespace Ecomerce.Pages_Produits
{
    public class CreateModel : PageModel
    {
        private readonly Ecomerce.Data.ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CreateModel(Ecomerce.Data.ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult OnGet()
        {
            ViewData["CategorieId"] = new SelectList(_context.Categories, "Id", "Nom");
            return Page();
        }

        [BindProperty]
        public Produit Produit { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Save the image file if one is uploaded
            if (Produit.ImageFile != null)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(Produit.ImageFile.FileName);
                var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await Produit.ImageFile.CopyToAsync(fileStream);
                }

                // Set the ImageUrl to the relative path of the saved image
                Produit.ImageUrl = "/images/" + fileName;
            }

            _context.Products.Add(Produit);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }

}