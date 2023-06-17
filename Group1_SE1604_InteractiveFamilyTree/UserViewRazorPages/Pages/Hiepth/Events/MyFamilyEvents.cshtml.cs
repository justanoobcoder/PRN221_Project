using BussinessObject.Models;
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

        public void OnGet()
        {
            int familyId = (int)userRepository.GetById(1).FamilyId;
            Events = eventRepository.GetByFamilyId(familyId);
        }
    }
}
