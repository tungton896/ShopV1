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
    public class DetailsModel : PageModel
    {
        private readonly WebShop.Data.WebShopContext _context;

        public DetailsModel(WebShop.Data.WebShopContext context)
        {
            _context = context;
        }

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
    }
}
