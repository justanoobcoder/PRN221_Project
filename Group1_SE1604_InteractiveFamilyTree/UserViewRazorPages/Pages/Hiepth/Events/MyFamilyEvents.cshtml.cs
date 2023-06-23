using BussinessObject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories.Hiepth;
using System.Collections.Generic;

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
                return NotFound();
            }
            int familyId = (int)userRepository.GetById(userId.Value).FamilyId;
            Events = eventRepository.GetByFamilyId(familyId);
            return Page();
        }
    }
}
