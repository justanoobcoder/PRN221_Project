using BussinessObject.Models;
using DataAcessObject.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories.Hiepth;
using System.Collections.Generic;

namespace UserViewRazorPages.Pages.Hiepth.Events
{
    [BindProperties]
    [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
    public class DetailModel : PageModel
    {
        private readonly IEventRepository eventRepository = new EventRepository();

        public List<User> Users { get; set; }
        public List<string> UserStatus { get; set; } = new List<string>();
        public int LoginUserId;

        public Event Event { get; set; }
        public bool CanJoin { get; set; } = false;

        public IActionResult OnGet(int id)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId is null)
            {
                return RedirectToPage("/Dangptm/Login");
            }
            LoginUserId = (int)userId;
            UserJoin uj = eventRepository.GetUserJoinByUserIdAndEventId(userId.Value, id);
            if (uj == null)
            {
                CanJoin = true;
            }
            else if (uj != null && uj.View == 0)
            {
                uj.View = 1;
                eventRepository.UpdateUserJoin(uj);
            }
            Event = eventRepository.GetByEventId(id);
            Users = eventRepository.GetUsersByEventId(id);
            foreach (var u in Users)
            {
                UserJoin userJoin = eventRepository.GetUserJoinByUserIdAndEventId(u.UserId, id);
                UserStatus.Add(userJoin.Status);
            }
            return Page();
        }

        public IActionResult OnPostRequestToJoin(string eventId)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId is null)
            {
                return RedirectToPage("/Dangptm/Login");
            }
            LoginUserId = (int)userId;
            eventRepository.RequestToJoinEvent(userId.Value, int.Parse(eventId));
            return RedirectToPage("Detail", new { id = int.Parse(eventId) });
        }

        public IActionResult OnPostModifyUserFromEvent(string action, string eventId, string userId)
        {
            if (!int.TryParse(eventId, out int eId))
            {
                return RedirectToPage("/Error");
            }
            if (!int.TryParse(userId, out int uId))
            {
                return RedirectToPage("/Error");
            }
            int? loginUserId = HttpContext.Session.GetInt32("UserId");
            if (userId is null)
            {
                return RedirectToPage("/Dangptm/Login");
            }
            LoginUserId = (int)loginUserId;
            switch (action)
            {
                case "delete":
                    eventRepository.RemoveUserFromEvent(eId, uId); break;
                case "deny":
                    UserJoin userJoin = eventRepository.GetUserJoinByUserIdAndEventId(uId, eId);
                    userJoin.Status = UserEventStatus.Denied.ToString();
                    eventRepository.UpdateUserJoin(userJoin);
                    break;
                default:
                    return RedirectToPage("/Error");
            }

            return RedirectToPage("Detail", new { id = eId });
        }
    }
}
