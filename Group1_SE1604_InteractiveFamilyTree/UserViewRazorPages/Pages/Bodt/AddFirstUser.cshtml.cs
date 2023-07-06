using BussinessObject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories.Bodt;
using Repositories.Bodt.Imple;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace UserViewRazorPages.Pages.Bodt
{
    public class AddFirstUserModel : PageModel
    {
        private readonly IWebHostEnvironment _environment;
        public AddFirstUserModel(IWebHostEnvironment environment)
        {
            _environment = environment;
        }
        IFamilyRepository familyRepository = new FamilyRepository();
        IUserRepository userRepository = new UserRepository();
        IRelationshipRepository relationshipRepository = new RelationshipRepository();
        [BindProperty]
        public User User { get; set; }
        public List<User> users { get; set; }
        [BindProperty]
        public string selectedGender { get; set; }
        [BindProperty]
        public IFormFile ImageFile { get; set; }
        [BindProperty]
        public int FamilyId { get; set; }
        public IActionResult OnGet(int familyId)
        {
            FamilyId = familyId;
            return Page();
        }
        public IActionResult OnPost()
        {
            User.FamilyId = FamilyId;
            User.Gender = (selectedGender == "Male") ? true : false;
            UploadImage(ImageFile);
            User.Code = RandomCodeGenerator.GenerateRandomCode();
            User.Status = "Using";
            userRepository.AddUser(User);
            int UserId = relationshipRepository.GetMainUser(User.FamilyId.GetValueOrDefault());
            HttpContext.Session.SetInt32("UserId", UserId);
            return RedirectToPage("/Bodt/MainPage");
        }
        private async Task<string> UploadImage(IFormFile ImageFile)
        {
            if (ImageFile != null && ImageFile.Length > 0)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(ImageFile.FileName);
                string directoryPath = Path.Combine(_environment.WebRootPath, "images");
                string imagePath = Path.Combine(directoryPath, fileName);

                // Create the directory if it doesn't exist
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(stream);
                }

                User.Img = "images/" + fileName;
                return User.Img;
            }

            return null;
        }
        private class RandomCodeGenerator
        {
            private static Random random = new Random();
            private const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

            public static string GenerateRandomCode()
            {
                string code = new string(Enumerable.Repeat(chars, 10)
                    .Select(s => s[random.Next(s.Length)])
                    .ToArray());

                return code;
            }
        }
    }
}
