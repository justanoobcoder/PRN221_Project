using BussinessObject.Models;
using Repositories.Dangptm;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;

namespace UserViewRazorPages.Pages.Dangptm
{
    public class LoginModel : PageModel
    {
        IUserRepository userRepository = new UserRepository();

        [BindProperty]
        public User User { get; set; } = default!;

        public IActionResult OnPost()
        {
            User loginUser = userRepository.Login(User.Email, User.Password);
            if (loginUser == null)
            {
                ViewData["notification"] = "Email or Password is wrong!";
                return Page();
            }
            else
            {
                int UserId = loginUser.UserId;
                HttpContext.Session.SetInt32("UserId", UserId);
                return RedirectToPage("/Bodt/MainPage");
            }

        }
        public IActionResult OnGetLogout()
        {
            HttpContext.Session.Remove("UserId");
            return Page();
        }
    }
}
