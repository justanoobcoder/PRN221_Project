using BussinessObject.Models;
using Repositories.Dangptm;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;

namespace UserViewRazorPages.Pages.Dangptm
{
    public class LoginAdminModel : PageModel
    {
        private readonly IUserRepository _userRepository;
        public LoginAdminModel(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [BindProperty]
        public Admin Admin { get; set; } = default!;

        public IActionResult OnPost()
        {
            Admin loginAdmin = _userRepository.LoginAdmin(Admin.Email, Admin.Password);
            if (loginAdmin == null)
            {
                ViewData["notification"] = "Email or Password is wrong!";
                return Page();
            }
            else
            {
                int AdminId = loginAdmin.AdminId;
                HttpContext.Session.SetInt32("AdminId", AdminId);
                return RedirectToPage("/AdminPages/FamilyManagement/FamilyManagement");
            }

        }
        public IActionResult OnGetLogout()
        {
            HttpContext.Session.Remove("AdminId");
            HttpContext.Session.Remove("cart");
            return Page();
        }
    }
}
