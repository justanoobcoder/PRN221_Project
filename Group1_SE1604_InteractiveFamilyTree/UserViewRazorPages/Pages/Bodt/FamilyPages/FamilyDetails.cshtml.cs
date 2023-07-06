using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BussinessObject.Models;
using Repositories.Bodt;
using Repositories.Bodt.Imple;
using Microsoft.AspNetCore.Http;

namespace UserViewRazorPages.Pages.Bodt.FamilyPages
{
    public class FamilyDetailsModel : PageModel
    {
        IUserRepository userRepository = new UserRepository();
        IFamilyRepository familyRepository = new FamilyRepository();    

        public Family Family { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            int userId = HttpContext.Session.GetInt32("UserId") ?? 0;
            if (userId == 0)
                return NotFound();
            User user = userRepository.GetUser(userId);

            Family = familyRepository.GetFamily(user.FamilyId.GetValueOrDefault());

            if (Family == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
