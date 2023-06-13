using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Data;
using WebShop.Areas.Identity.Data;

namespace Album.Areas.Admin.Pages.User
{
    [Authorize(Roles = "Admin")]
    public class UpdateUser : PageModel
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<WebShopUser> _userManager;


        public UpdateUser(RoleManager<IdentityRole> roleManager,
                            UserManager<WebShopUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public class InputModel
        {
            [Required]
            public string ID { set; get; }

            [Required(ErrorMessage = "Phải nhập thông tin tên người dùng")]
            [Display(Name = "Tên người dùng")]
            [StringLength(100, ErrorMessage = "{0} dài {2} đến {1} ký tự.", MinimumLength = 3)]
            public string FullName { set; get; }

            [Required(ErrorMessage = "Phải nhập thông tin email")]
            [Display(Name = "Email")]
            public string Email { set; get; }

            [Required(ErrorMessage = "Phải nhập số điện thoại")]
            [Display(Name = "Số điện thoại")]
            [MaxLength(10, ErrorMessage = "Số điện thoại bao gồm 10 chữ số")]
            public string PhoneNumber { set; get; }

        }

        [BindProperty]
        public InputModel Input { set; get; }

        [BindProperty]
        public bool isConfirmed { set; get; }

        [TempData] // Sử dụng Session
        public string StatusMessage { get; set; }

        public IActionResult OnGet() => NotFound("Không thấy");

        public async Task<IActionResult> OnPost()
        {


            var user = await _userManager.FindByIdAsync(Input.ID);
            if (user == null)
            {
                return NotFound("Không thấy người dùng");
            }

            if (!isConfirmed)
            {
                isConfirmed = true;
                StatusMessage = "";
                ModelState.Clear();

                Input.Email = user.Email;
                Input.FullName = user.FullName != null ? user.FullName : string.Empty;
                Input.PhoneNumber = user.PhoneNumber;
            }
            else
            {
                // Update add and remove
                StatusMessage = "Vừa cập nhật";
                var newUser = user;
                if (Input.Email != newUser.Email)
                    newUser.Email = Input.Email;
                
                if (Input.FullName != newUser.FullName)
                    newUser.FullName = Input.FullName;
                
                if (Input.PhoneNumber != newUser.PhoneNumber)
                    newUser.PhoneNumber = Input.PhoneNumber;
                await _userManager.UpdateAsync(newUser);
                
            }
            return Page();
        }
    }
}