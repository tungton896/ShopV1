using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebShop.Models;

namespace WebShop.Areas.Admin.Pages.Category
{
    public class IndexModel : PageModel
    {
        private readonly WebShop.Data.WebShopContext _context;

        public IndexModel(WebShop.Data.WebShopContext context)
        {
            _context = context;
        }

        public IList<WebShop.Models.Category> Category { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.categorys != null)
            {
                Category = await _context.categorys.ToListAsync();
            }
        }
    }
}
