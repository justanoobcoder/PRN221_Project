using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace UserViewRazorPages.Pages.Bodt
{
    public class RelationModel : PageModel
    {
        [BindProperty]
        public string SelectedOption { get; set; }

        public IActionResult OnPost()
        {
            int userId = HttpContext.Session.GetInt32("UserId") ?? 0;
            if (userId == 0)
                return NotFound();
            return RedirectToPage("/Bodt/Create", new { option = SelectedOption });
        }

    }
}

