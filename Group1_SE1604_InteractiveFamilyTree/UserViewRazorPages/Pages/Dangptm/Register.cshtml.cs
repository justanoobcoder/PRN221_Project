using BussinessObject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repositories.Dangptm;
using System;
using System.Text;

namespace UserViewRazorPages.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly IUserRepository _userRepository;
        private readonly IFamilyRepository _familyRepository;
        public RegisterModel(IUserRepository userRepository, IFamilyRepository familyRepository)
        {
            _userRepository = userRepository;
            _familyRepository = familyRepository;
        }
        [BindProperty]
        public User User { get; set; } = default!;

        [BindProperty]
        public string selectedOption { get; set; } = default!;
        //public IActionResult Onget()
        //{
        //    ViewData["FamilyId"] = new SelectList(_familyRepository.GetFamilieList(), "FamilyId", "Name");
        //    return Page();
        //}
        public IActionResult OnPost()
        {
            try
            {
                User.Gender = (selectedOption == "Male") ? true : false;
                User.Status = "Using";
                String date = User.Birthday.ToString();
                User.Code = getAbbreviation(User.Name, User.Birthday.ToString());
                _userRepository.AddUser(User);
                int UserId = User.UserId;
                HttpContext.Session.SetInt32("UserId", UserId);
                ViewData["notification"] = "Successfully!!";
                return Page();
            }
            catch (Exception ex)
            {
                ViewData["notification"] = ex.Message;
                return Page();
            }
        }

        private String getAbbreviation(String name, String birthdate)
        {
            var abbreviation = new System.Text.StringBuilder();
            string[] words1 = birthdate.Split('/');

            string[] words2 = name.Split(' ');

            foreach (string word in words2)
            {
                if (!string.IsNullOrEmpty(word))
                {
                    char firstLetter = char.ToUpper(word[0]);
                    abbreviation.Append(firstLetter);
                }
            }
            foreach (string word in words1)
            {
                if (!string.IsNullOrEmpty(word))
                {
                    char firstLetter = char.ToUpper(word[0]);
                    abbreviation.Append(firstLetter);
                }
            }
            return abbreviation.ToString();
        }
    }
}
