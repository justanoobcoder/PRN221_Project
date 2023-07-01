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

namespace UserViewRazorPages.Pages.AdminPages.FamilyManagement
{
    public class FamilyManagementModel : PageModel
    {
        IFamilyRepository familyRepository = new FamilyRepository();

        public IList<Family> Family { get;set; }

        public IActionResult OnGet()
        {
            Family = familyRepository.GetAllFamilies();
            return Page();
        }
    }
}
