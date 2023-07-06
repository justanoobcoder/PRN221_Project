using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BussinessObject.Models;
using Repositories.Bodt.Imple;
using Repositories.Bodt;
using Microsoft.AspNetCore.Http;

namespace UserViewRazorPages.Pages.Bodt.FamilyPages
{
    public class EditFamilyProfileModel : PageModel
    {
        IFamilyRepository familyRepository = new FamilyRepository();

        [BindProperty]
        public Family Family { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            int userId = HttpContext.Session.GetInt32("UserId") ?? 0;
            if (userId == 0)
                return NotFound();
            if (id == null)
            {
                return NotFound();
            }

            Family = familyRepository.GetFamily(id.GetValueOrDefault());

            if (Family == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            try
            {
                familyRepository.EditFamilyDetail(Family);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FamilyExists(Family.FamilyId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("/Bodt/FamilyPages/FamilyDetails");
        }

        private bool FamilyExists(int id)
        {
            Family family = familyRepository.GetFamily(id);
            if (family == null)
                return false;
            return true;
        }
    }
}
