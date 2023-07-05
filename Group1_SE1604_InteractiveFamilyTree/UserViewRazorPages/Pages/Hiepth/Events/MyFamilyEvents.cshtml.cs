using BussinessObject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories.Hiepth;
using System.Collections.Generic;
using System.Linq;

namespace UserViewRazorPages.Pages.Hiepth.Events
{
    [BindProperties]
    public class MyFamilyEventsModel : PageModel
    {
        private readonly IEventRepository eventRepository = new EventRepository();
        private readonly IUserRepository userRepository = new UserRepository();

        public List<Event> Events { get; set; }

        public IActionResult OnGet()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId is null)
            {
                return RedirectToPage("/Dangptm/Login");
            }
            int familyId = (int)userRepository.GetById(userId.Value).FamilyId;
            Events = eventRepository.GetByFamilyId(familyId).OrderByDescending(e => e.StartDate).ToList();
            return Page();
        }

        public IActionResult OnPostSearch(string value)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId is null)
            {
                return RedirectToPage("/Dangptm/Login");
            }
            int familyId = (int)userRepository.GetById(userId.Value).FamilyId;
            Events = eventRepository.GetByFamilyId(familyId)
                .OrderByDescending(e => e.StartDate)
                .ToList();
            if (value is not null)
            {
                Events = Events
                    .Where(e => e.EventName.ToLower().Contains(value.ToLower().Trim()))
                    .ToList();
            }
            return Page();
        }
    }
}
