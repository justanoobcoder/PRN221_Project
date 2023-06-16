using BussinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories.Hiepth;
using System.Collections.Generic;

namespace UserViewRazorPages.Pages.Hiepth.Events
{
    public class ListAllModel : PageModel
    {
        private readonly IEventRepository eventRepository = new EventRepository();

        [BindProperty]
        public IList<Event> Events { get; set; }

        public IActionResult OnGet()
        {
            Events = eventRepository.GetAll();
            return Page();
        }
    }
}
