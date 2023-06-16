using BussinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories.Hiepth;
using System.Collections.Generic;

namespace UserViewRazorPages.Pages.Hiepth.Events
{
    [BindProperties]
    public class DetailModel : PageModel
    {
        private readonly IEventRepository eventRepository = new EventRepository();

        public List<User> Users { get; set; }

        public Event Event { get; set; }

        public void OnGet(int id)
        {
            Event = eventRepository.GetByEventId(id);
            Users = eventRepository.GetUsersByEventId(id);
        }
    }
}
