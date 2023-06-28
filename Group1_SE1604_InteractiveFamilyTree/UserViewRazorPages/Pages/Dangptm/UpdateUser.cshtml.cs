using BussinessObject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories.Dangptm;
using System;

namespace UserViewRazorPages.Pages.Dangptm
{
    public class UpdateUserModel : PageModel
    {
        private readonly IUserRepository _userRepository;
        private readonly IFamilyRepository _familyRepository;
        public UpdateUserModel(IUserRepository userRepository, IFamilyRepository familyRepository)
        {
            _userRepository = userRepository;
            _familyRepository = familyRepository;
        }
        [BindProperty]
        public User User { get; set; } = default!;

        [BindProperty]
        public string selectedOption { get; set; } = default!;
        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetInt32("UserId") != null)
            {
                User = _userRepository.GetUser((int)(HttpContext.Session.GetInt32("UserId")));
                selectedOption = (User.Gender == true) ? "Male" : "Female";
                return Page();
            }
            return NotFound();            
        }

        public IActionResult OnPost()
        {
            try
            {
                User.Gender = (selectedOption == "Male") ? true : false;
                User.Status = "Using";
                _userRepository.Update(User);
                return Redirect("/Bodt/MainPage");
            }
            catch (Exception ex)
            {
                ViewData["notification"] = ex.Message;
                return Page();
            }
        }
    }
}
