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
            return RedirectToPage("/Bodt/Create", new { option = SelectedOption });
        }

    }
}

