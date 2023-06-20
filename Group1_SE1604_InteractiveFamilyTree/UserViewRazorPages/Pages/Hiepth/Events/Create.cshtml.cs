using BussinessObject.Models;
using DataAcessObject.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories.Hiepth;

namespace UserViewRazorPages.Pages.Hiepth.Events
{
    public class CreateModel : PageModel
    {
        private readonly IEventRepository eventRepository = new EventRepository();

        [BindProperty]
        public Event Event { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public IActionResult OnPost()
        {
            Event.CreatorId = 1;
            Event.Status = EventStatus.Waiting.ToString();

            int newEventId = eventRepository.Save(Event);

            return RedirectToPage("AddMember", new { eventId = newEventId });
        }
    }
}
