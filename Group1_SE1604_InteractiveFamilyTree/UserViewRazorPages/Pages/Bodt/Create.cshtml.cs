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

namespace UserViewRazorPages.Pages.Bodt
{
    public class CreateModel : PageModel
    {
        IRelationshipRepository relationshipRepository = new RelationshipRepository();
        IUserRepository userRepository = new UserRepository();
        [BindProperty]
        public int SelectedUserId { get; set; }
        public List<User> users { get; set; }
        [BindProperty]
        public User User { get; set; }
        [BindProperty]
        public string SelectedOption { get; set; }
        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public IActionResult OnGet(string option)
        {
            SelectedOption = option;
            users = userRepository.GetUserListByFamilyId(1);
            List<User> doNotIncludedUsers;
            if (SelectedOption.Equals("Option1"))
            {
                doNotIncludedUsers = userRepository.getMarriedUser(1);
                
            }
            else
            {
                doNotIncludedUsers = userRepository.getUnavailable(1);
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
            User.FamilyId = 1;
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
    }
}
