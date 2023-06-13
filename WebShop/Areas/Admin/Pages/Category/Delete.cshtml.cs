using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebShop.Data;
using WebShop.Models;

namespace WebShop.Areas.Admin.Pages.Category
{
    public class DeleteModel : PageModel
    {
        private readonly WebShop.Data.WebShopContext _context;

        public DeleteModel(WebShop.Data.WebShopContext context)
        {
            _context = context;
        }

        [BindProperty]
      public WebShop.Models.Category Category { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.categorys == null)
            {
                return NotFound();
            }

            var category = await _context.categorys.FirstOrDefaultAsync(m => m.CategoryId == id);

            if (category == null)
            {
                return NotFound();
            }
            else 
            {
                Category = category;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.categorys == null)
            {
                return NotFound();
            }
            var category = await _context.categorys.FindAsync(id);

            if (category != null)
            {
                Category = category;
                _context.categorys.Remove(Category);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
