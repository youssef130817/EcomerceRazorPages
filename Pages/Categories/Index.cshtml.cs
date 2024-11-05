using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Ecomerce.Data;
using Ecomerce.Models;

namespace Ecomerce.Pages_Categories
{
    public class IndexModel : PageModel
    {
        private readonly Ecomerce.Data.ApplicationDbContext _context;

        public IndexModel(Ecomerce.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Categorie> Categorie { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Categorie = await _context.Categories.ToListAsync();
        }
    }
}
