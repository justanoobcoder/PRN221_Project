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

namespace UserViewRazorPages.Pages.Bodt
{
    public class DeleteModel : PageModel
    {
        IUserRepository userRepository = new UserRepository();
        IRelationshipRepository relationshipRepository = new RelationshipRepository();
        [BindProperty]
        public User User { get; set; }

        public IActionResult OnGet(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            User = userRepository.GetUser(id);

            if (User == null)
            {
                return NotFound();
            }
            return Page();
        }

        public IActionResult OnPost(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            User = userRepository.GetUser(id);

            if (User != null)
            {
                relationshipRepository.Delete(id);
            }

            return RedirectToPage("/Bodt/MainPage");
        }
    }
}
