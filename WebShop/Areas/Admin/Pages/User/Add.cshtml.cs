using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using WebShop.Areas.Identity.Data;

namespace WebShop.Areas.Admin.Pages.User
{
    public class AddModel : PageModel
    {
        private readonly UserManager<WebShopUser> _userManager;

        public AddModel(UserManager<WebShopUser> userManager)
        {
            _userManager = userManager;
        }

        [TempData] // Sử dụng Session
        public string StatusMessage { get; set; }

        public class InputModel
        {
            public string? ID { set; get; }

            [Required(ErrorMessage = "Phải nhập thông tin tên người dùng")]
            [Display(Name = "Tên người dùng")]
            [StringLength(100, ErrorMessage = "{0} dài {2} đến {1} ký tự.", MinimumLength = 3)]
            public string FullName { set; get; }

            [Required(ErrorMessage = "Phải nhập thông tin email")]
            [Display(Name = "Email")]
            public string Email { set; get; }

            [Required(ErrorMessage = "Phải nhập số điện thoại")]
            [Display(Name = "Số điện thoại")]
            public string PhoneNumber { set; get; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }
        }

        [BindProperty]
        public InputModel Input { set; get; }

        [BindProperty]
        public bool IsUpdate { set; get; }

        // Không cho truy cập trang mặc định mà không có handler
        public IActionResult OnGet() => NotFound("Không thấy");
        public IActionResult OnPost() => NotFound("Không thấy");


        public IActionResult OnPostStartNewUser()
        {
            StatusMessage = "Hãy nhập thông tin để tạo người dùng mới";
            IsUpdate = false;
            ModelState.Clear();
            return Page();
        }

        // Truy vấn lấy thông tin Role cần cập nhật
        public async Task<IActionResult> OnPostStartUpdate()
        {
            StatusMessage = null;
            IsUpdate = true;
            if (Input.ID == null)
            {
                StatusMessage = "Error: Không có thông tin về người dùng";
                return Page();
            }
            var result = await _userManager.FindByIdAsync(Input.ID);
            if (result != null)
            {
                Input.FullName = result.FullName;
                ViewData["Title"] = "Cập nhật thông tin : " + Input.FullName;
                ModelState.Clear();
            }
            else
            {
                StatusMessage = "Error: Không có thông tin về người dùng ID = " + Input.ID;
            }

            return Page();
        }

        // Cập nhật hoặc thêm mới tùy thuộc vào IsUpdate
        public async Task<IActionResult> OnPostAddOrUpdate()
        {

            if (!ModelState.IsValid)
            {
                StatusMessage = null;
                return Page();
            }

            if (IsUpdate)
            {
                // CẬP NHẬT
                if (Input.ID == null)
                {
                    ModelState.Clear();
                    StatusMessage = "Error: Không có thông tin về người dùng";
                    return Page();
                }
                var result = await _userManager.FindByIdAsync(Input.ID);
                if (result != null)
                {
                    result.FullName = Input.FullName;
                    // Cập nhật tên Role
                    var roleUpdateRs = await _userManager.UpdateAsync(result);
                    if (roleUpdateRs.Succeeded)
                    {
                        StatusMessage = "Đã cập nhật role thành công";
                    }
                    else
                    {
                        StatusMessage = "Error: ";
                        foreach (var er in roleUpdateRs.Errors)
                        {
                            StatusMessage += er.Description;
                        }
                    }
                }
                else
                {
                    StatusMessage = "Error: Không tìm thấy Role cập nhật";
                }

            }
            else
            {
                // TẠO MỚI
                var newUser = new WebShopUser
                {
                    Email = Input.Email,
                    FullName= Input.FullName,
                    PhoneNumber= Input.PhoneNumber,
                    UserName= Input.Email,
                };
                // Thực hiện tạo Role mới
                var rsNewUser = await _userManager.CreateAsync(newUser);
                if (rsNewUser.Succeeded)
                {
                    StatusMessage = $"Đã tạo người dùng mới thành công: {newUser.FullName}";
                    return RedirectToPage("./Index");
                }
                else
                {
                    StatusMessage = "Error: Tạo người dùng mới không thành công";
                }
            }

            return Page();

        }
    }
}