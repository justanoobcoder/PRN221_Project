using BussinessObject.Models;
using DataAcessObject.Common;
using Microsoft.AspNetCore.Http;
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
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId is null)
            {
                return RedirectToPage("/Dangptm/Login");
            }
            return Page();
        }

        public IActionResult OnPost()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId is null)
            {
                return RedirectToPage("/Dangptm/Login");
            }
            Event.CreatorId = userId.Value;
            Event.Status = EventStatus.Waiting.ToString();

            int newEventId = eventRepository.Save(Event);

            return RedirectToPage("AddMember", new { eventId = newEventId });
        }
    }
}
