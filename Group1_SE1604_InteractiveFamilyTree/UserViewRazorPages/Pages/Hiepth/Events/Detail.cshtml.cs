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
        public List<string> UserStatus { get; set; } = new List<string>();

        public Event Event { get; set; }

        public void OnGet(int id)
        {
            UserJoin uj = eventRepository.GetUserJoinByUserIdAndEventId(1, id);
            uj.View = 1;
            eventRepository.UpdateUserJoin(uj);
            Event = eventRepository.GetByEventId(id);
            Users = eventRepository.GetUsersByEventId(id);
            foreach (var u in Users)
            {
                UserJoin userJoin = eventRepository.GetUserJoinByUserIdAndEventId(u.UserId, id);
                UserStatus.Add(userJoin.Status);
            }
        }
    }
}
