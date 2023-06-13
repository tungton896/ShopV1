using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebShop.Data;
using WebShop.Models;

namespace WebShop.Areas.Admin.Pages.Category
{
    public class CreateModel : PageModel
    {
        private readonly WebShop.Data.WebShopContext _context;

        public CreateModel(WebShop.Data.WebShopContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public WebShop.Models.Category Category { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.categorys == null || Category == null)
            {
                return Page();
            }

            _context.categorys.Add(Category);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
