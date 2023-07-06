using BussinessObject.Models;
using DataAcessObject.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories.Hiepth;

namespace UserViewRazorPages.Pages.Hiepth.Events
{
    public class UpdateModel : PageModel
    {
        private readonly IEventRepository eventRepository = new EventRepository();

        [BindProperty]
        public Event Event { get; set; }

        public IActionResult OnGet(int eventId)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId is null)
            {
                return RedirectToPage("/Dangptm/Login");
            }
            Event = eventRepository.GetByEventId(eventId);
            return Page();
        }

        public IActionResult OnPost(string eventId)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId is null)
            {
                return RedirectToPage("/Dangptm/Login");
            }
            if (Event.StartDate >= Event.EndDate)
            {
                ModelState.AddModelError("Event.EndDate", "End Date must be after Start Date");
                return Page();
            }

            if (!int.TryParse(eventId, out int eId))
            {
                return RedirectToPage("/Error");
            }
            var e = eventRepository.GetByEventId(eId);
            e.EventName = Event.EventName;
            e.Information = Event.Information;
            e.StartDate = Event.StartDate;
            e.EndDate = Event.EndDate;
            e.Location = Event.Location;
            eventRepository.Update(e);

            return RedirectToPage("Detail", new { id = eventId });
        }
    }
}
