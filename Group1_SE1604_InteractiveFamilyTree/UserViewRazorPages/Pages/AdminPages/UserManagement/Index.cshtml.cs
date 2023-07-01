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

namespace UserViewRazorPages.Pages.AdminPages.UserManagement
{
    public class IndexModel : PageModel
    {
        IUserRepository userRepository = new UserRepository();
        IFamilyRepository familyRepository = new FamilyRepository();    
        public IList<User> User { get;set; }
        public IList<Family> Families { get; set; }
        public IActionResult OnGet()
        {
            Families = familyRepository.GetAllFamilies();
            User = userRepository.GetUserListByFamilyId(1);
            return Page(); 
        }
        public IActionResult OnPost(string family)
        {
            Families = familyRepository.GetAllFamilies();
            User = userRepository.GetUserListByFamilyId(int.Parse(family));
            return Page();
        }
    }
}
