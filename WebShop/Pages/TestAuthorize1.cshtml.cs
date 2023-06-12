using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebShop.Pages
{
    [Authorize]
    public class TestAuthorize1Model : PageModel
    {
        public void OnGet()
        {
        }
    }
}
