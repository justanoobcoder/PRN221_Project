using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BussinessObject.Models;
using Repositories.Bodt.Imple;
using Repositories.Bodt;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace UserViewRazorPages.Pages.Bodt
{
    public class CreateModel : PageModel
    {
        private readonly IWebHostEnvironment _environment;
        public CreateModel(IWebHostEnvironment environment)
        {
            _environment = environment;
        }
        IRelationshipRepository relationshipRepository = new RelationshipRepository();
        IUserRepository userRepository = new UserRepository();
        [BindProperty]
        public int SelectedUserId { get; set; }
        public List<User> users { get; set; }
        [BindProperty]
        public IFormFile ImageFile { get; set; }
        [BindProperty]
        public User User { get; set; }
        [BindProperty]
        public string SelectedOption { get; set; }
        [BindProperty]
        public string selectedGender { get; set; }
        public IActionResult OnGet(string option)
        {
            int userId = HttpContext.Session.GetInt32("UserId") ?? 0;
            if (userId == 0)
                return NotFound();
            User loginUser = userRepository.GetUser(userId);
            SelectedOption = option;
            users = userRepository.GetUserListByFamilyId(loginUser.FamilyId.GetValueOrDefault());
            List<User> doNotIncludedUsers;
            if (SelectedOption.Equals("Option1"))
            {
                doNotIncludedUsers = userRepository.getMarriedUser(loginUser.FamilyId.GetValueOrDefault());
                
            }
            else
            {
                doNotIncludedUsers = userRepository.getUnavailable(loginUser.FamilyId.GetValueOrDefault());
            }
            foreach (var user in users.ToList())
            {
                foreach (var user2 in doNotIncludedUsers.ToList())
                {
                    if (user.UserId == user2.UserId)
                    {
                        users.Remove(user);
                        doNotIncludedUsers.Remove(user2);
                    }
                }
            }
            return Page();
        }

        public IActionResult OnPost()
        {
            int userId = HttpContext.Session.GetInt32("UserId") ?? 0;
            User loginUser = userRepository.GetUser(userId);
            User.FamilyId = loginUser.FamilyId;
            User.Gender = (selectedGender == "Male") ? true : false;
            UploadImage(ImageFile);
            RandomCodeGenerator randomCodeGenerator = new RandomCodeGenerator();
            User.Code = randomCodeGenerator.GenerateRandomCode();
            User.Status = "Not used";
            userRepository.AddUser(User);
            Relationship relationship = new Relationship();
            relationship.RelationshipId = relationshipRepository.GetNextRelationshipId();
            relationship.UserId1 = SelectedUserId;
            relationship.UserId2 = User.UserId;
            if (SelectedOption.Equals("Option1"))
            {
                relationship.RelationshipDetailId = 3;
                relationshipRepository.AddRelationship(relationship);
            }
            else
            {
                relationship.RelationshipDetailId = 1;
                relationshipRepository.AddRelationship(relationship);
            }

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
            IUserRepository _userRepository = new UserRepository();
            private static Random random = new Random();
            private const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

            public string GenerateRandomCode()
            {
                string code = new string(Enumerable.Repeat(chars, 10)
                    .Select(s => s[random.Next(s.Length)])
                    .ToArray());
                if (!_userRepository.CheckUserCodeIsValid(code)) 
                { 
                return GenerateRandomCode();
                }
                return code;
            }
        }
    }
}
