using BussinessObject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories.Dangptm;

namespace UserViewRazorPages.Pages
{
    public class PreRegisterModel : PageModel
    {
        private readonly IUserRepository _userRepository;
        public PreRegisterModel(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [BindProperty]
        public User User { get; set; } = default!;

        [BindProperty]
        public string Code { get; set; } = default!;

        public IActionResult OnPost()
        {
            User user = _userRepository.GetUserByCode(Code);
            if (user == null)
            {
                ViewData["notification"] = "User Code is wrong!";
                return Page();
            }
            else
            {
                if (user.Email == null)
                {
                    User = user;
                    User.Code = Code;
                    HttpContext.Session.SetInt32("UserId", User.UserId);
                    ViewData["notification"] = "Ngon!";
                    return RedirectToPage("./UpdateUser");
                }
                else
                {
                    ViewData["notification"] = "Account already has existed!";
                    return Page();
                }
                
            }

        }
    }
}
