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
    public class CreateNewFamilyModel : PageModel
    {
        IFamilyRepository familyRepository = new FamilyRepository();
        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Family Family { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public IActionResult OnPost()
        {
            int newFamilyId = familyRepository.CreateNewFamily(Family);

            return RedirectToPage("/Bodt/AddFirstUser", new { familyId = newFamilyId });
        }
    }
}
