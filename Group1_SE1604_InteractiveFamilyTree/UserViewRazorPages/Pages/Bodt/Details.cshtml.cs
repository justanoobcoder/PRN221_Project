using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BussinessObject.Models;
using Repositories.Bodt.Imple;
using Repositories.Bodt;
using Microsoft.AspNetCore.Http;

namespace UserViewRazorPages.Pages.Bodt
{
    public class DetailsModel : PageModel
    {
        IUserRepository userRepository = new UserRepository();
        public User User { get; set; }
        [BindProperty]
        public int UserId { get; set; }
        public IActionResult OnGet(int id)
        {
            UserId = HttpContext.Session.GetInt32("UserId") ?? 0;
            if (UserId == 0)
                return NotFound();
            User = userRepository.GetUser(id);
            if (User.Img == null)
                User.Img = "images/User.jpg";
            return Page();
        }
    }
}
