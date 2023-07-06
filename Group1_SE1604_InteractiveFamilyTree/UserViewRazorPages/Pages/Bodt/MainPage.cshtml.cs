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
using RazorPage.ViewModels;
using Microsoft.AspNetCore.Http;
using System.Data;
using System.IO;

namespace UserViewRazorPages.Pages.Bodt
{
    public class IndexModel : PageModel
    {
        IRelationshipRepository relationshipRepository = new RelationshipRepository();
        IUserRepository userRepository = new UserRepository();

        public List<User> Users { get;set; }
        public int MainUser { get; set; }
        public User partnerOfMain { get; set; }

        public IActionResult OnGet()
        {
            int userId = HttpContext.Session.GetInt32("UserId") ?? 0;
            if (userId == 0)
                return NotFound();
            User loginUser = userRepository.GetUser(userId);
            MainUser = relationshipRepository.GetMainUser(loginUser.FamilyId.GetValueOrDefault());
            Users = userRepository.GetUserListByFamilyId(loginUser.FamilyId.GetValueOrDefault());
            string imagePath;
            byte[] imageData;
            string base64Image;
            string dataUri;
            foreach (User user in Users)
            {
                user.PartnerId = relationshipRepository.getPartner(user.UserId);
                if (user.Img == null)
                    user.Img = "images/User.jpg";
                //show image
                
                List<int> relationship = relationshipRepository.GetRelationship(user.UserId, 1);
                if (user.UserId == MainUser)
                {
                    MainUser = Users.IndexOf(user);
                }
                if (relationship == null || relationship.Count == 0)
                    continue;   
                List<User> users = new List<User>();
                for (int i = 0; i < relationship.Count; i++)
                {
                    users.Add(userRepository.GetUser(relationship[i]));
                }
                user.Children = users;
            }
            if (Users[MainUser].PartnerId != 0)
            {
                partnerOfMain = userRepository.GetUser(Users[MainUser].PartnerId);
            }
            else partnerOfMain = null;
            return Page();
        }
    }
}

