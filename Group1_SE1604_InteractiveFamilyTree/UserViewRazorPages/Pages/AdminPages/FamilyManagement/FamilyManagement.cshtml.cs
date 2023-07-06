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

namespace UserViewRazorPages.Pages.AdminPages.FamilyManagement
{
    public class FamilyManagementModel : PageModel
    {
        IFamilyRepository familyRepository = new FamilyRepository();

        public IList<Family> Family { get;set; }

        public IActionResult OnGet()
        {
            int adminId = HttpContext.Session.GetInt32("AdminId") ?? 0;
            if (adminId == 0)
            {
                return NotFound();
            }

            Family = familyRepository.GetAllFamilies();
            return Page();
        }
    }
}
