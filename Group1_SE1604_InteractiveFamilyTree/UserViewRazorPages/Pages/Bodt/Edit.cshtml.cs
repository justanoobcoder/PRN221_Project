using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BussinessObject.Models;
using Repositories.Bodt.Imple;
using Repositories.Bodt;
using Microsoft.AspNetCore.Http;
using RazorPage.ViewModels;

namespace UserViewRazorPages.Pages.Bodt
{
    public class EditModel : PageModel
    {
        IUserRepository userRepository = new UserRepository();
        public string PasswordConfirm { get; set; }
        [BindProperty]
        public User User { get; set; }
        [BindProperty]
        public string selectedGender { get; set; }
        public IActionResult OnGet()
        {
            int userId = HttpContext.Session.GetInt32("UserId") ?? 0;
            if (userId == 0)
            {
                return NotFound();
            }
            User = userRepository.GetUser(userId);
            if (User == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public IActionResult OnPost()
        {
            String confirmPassword = Request.Form["ConfirmPassword"];
            int userId = HttpContext.Session.GetInt32("UserId") ?? 0;
            if (userId != 0 && confirmPassword.Equals(User.Password))
            {
                User.Gender = (selectedGender == "Male") ? true : false;
                ModelState.AddModelError(string.Empty, "Success!");
                userRepository.Update(User);
                return Page();
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Wrong inpur, or password and password confirm do not match.");
                return Page();
            }
        }

        private bool UserExists(int id)
        {
            User userCheck = userRepository.GetUser(id);
            if (userCheck != null)
                return true;
            return false;

        }
    }
}
