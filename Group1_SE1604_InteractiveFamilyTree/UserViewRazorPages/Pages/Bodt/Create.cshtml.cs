using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BussinessObject.Models;

namespace UserViewRazorPages.Pages.Bodt
{
    public class CreateModel : PageModel
    {
        private readonly BussinessObject.Models.FamilyTreeContext _context;

        public CreateModel(BussinessObject.Models.FamilyTreeContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["FamilyId"] = new SelectList(_context.Families, "FamilyId", "Address");
            return Page();
        }

        [BindProperty]
        public User User { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Users.Add(User);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
