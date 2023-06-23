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
            User loginUser = userRepository.GetUser(userId);
            MainUser = relationshipRepository.GetMainUser(loginUser.FamilyId.GetValueOrDefault());
            Users = userRepository.GetUserListByFamilyId(loginUser.FamilyId.GetValueOrDefault()).ToList();
            foreach (User user in Users)
            {
                user.PartnerId = relationshipRepository.getPartner(user.UserId);
                List<int> relationship = relationshipRepository.GetRelationship(user.UserId, 1);
                if (relationship == null || relationship.Count == 0)
                    continue;
                if (user.UserId == MainUser)
                {
                    MainUser = Users.IndexOf(user);
                }
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
            return Page();
        }
    }
}

