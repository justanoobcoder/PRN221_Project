using BussinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories.Hiepth;
using System.Collections.Generic;
using System.Linq;

namespace UserViewRazorPages.Pages.Hiepth.Events
{
    public class MyEventsModel : PageModel
    {
        private readonly IEventRepository eventRepository = new EventRepository();

        public IList<Event> Events { get; set; }

        public IActionResult OnGet()
        {
            Events = eventRepository.GetByUserId(1);
            return Page();
        }
    }
}
