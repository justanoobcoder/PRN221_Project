using BussinessObject.Models;
using DataAcessObject.Common;
using Microsoft.AspNetCore.Http;
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
        public List<string> UserStatus { get; set; } = new List<string>();

        public IActionResult OnGet()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId is null)
            {
                return RedirectToPage("/Dangptm/Login");
            }
            Events = eventRepository.GetByUserId(userId.Value).OrderByDescending(e => e.StartDate).ToList();
            foreach (var e in Events)
            {
                UserJoin userJoin = eventRepository.GetUserJoinByUserIdAndEventId(userId.Value, e.EventId);
                UserStatus.Add(userJoin.Status);
            }
            return Page();
        }

        public IActionResult OnPostSearch(string value)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId is null)
            {
                return RedirectToPage("/Dangptm/Login");
            }
            Events = eventRepository.GetByUserId(userId.Value)
                .OrderByDescending(e => e.StartDate)
                .ToList();
            if (value is not null)
            {
                Events = Events
                    .Where(e => e.EventName.ToLower().Contains(value.ToLower().Trim()))
                    .ToList();
            }
            foreach (var e in Events)
            {
                UserJoin userJoin = eventRepository.GetUserJoinByUserIdAndEventId(userId.Value, e.EventId);
                UserStatus.Add(userJoin.Status);
            }
            return Page();
        }

        public IActionResult OnPostAcceptEvent(string eventId)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId is null)
            {
                return RedirectToPage("/Dangptm/Login");
            }
            UserJoin userJoin = eventRepository.GetUserJoinByUserIdAndEventId(userId.Value, int.Parse(eventId));
            userJoin.Status = UserEventStatus.Accepted.ToString();
            eventRepository.UpdateUserJoin(userJoin);
            return RedirectToPage("MyEvents");
        }

        public IActionResult OnPostDenyEvent(string eventId)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId is null)
            {
                return RedirectToPage("/Dangptm/Login");
            }
            UserJoin userJoin = eventRepository.GetUserJoinByUserIdAndEventId(userId.Value, int.Parse(eventId));
            userJoin.Status = UserEventStatus.Denied.ToString();
            eventRepository.UpdateUserJoin(userJoin);
            return RedirectToPage("MyEvents");
        }
    }
}
