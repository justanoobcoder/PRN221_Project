using BussinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories.Hiepth;
using System.Collections.Generic;

namespace UserViewRazorPages.Pages.Hiepth.Events
{
    [BindProperties]
    public class ListAllModel : PageModel
    {
        private readonly IEventRepository eventRepository = new EventRepository();

        public IList<Event> Events { get; set; }
        public int UnseenEventCount { get; set; }

        public IActionResult OnGet()
        {
            UnseenEventCount = eventRepository.GetUnseenEventCountByUserId(1);
            Events = eventRepository.GetAll();
            return Page();
        }
    }
}
