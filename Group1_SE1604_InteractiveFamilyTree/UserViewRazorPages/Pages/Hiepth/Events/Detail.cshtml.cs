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
            Event = eventRepository.GetByEventId(id);
            Users = eventRepository.GetUsersByEventId(id);
            UserJoin uj = eventRepository.GetUserJoinByUserIdAndEventId(userId.Value, id);
            if (uj == null && Event.Status.Equals(EventStatus.Waiting.ToString()))
            {
                CanJoin = true;
            }
            else if (uj != null && uj.View == 0)
            {
                uj.View = 1;
                eventRepository.UpdateUserJoin(uj);
            }
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
            if (loginUserId is null)
            {
                return RedirectToPage("/Dangptm/Login");
            }
            LoginUserId = (int)loginUserId;
            UserJoin userJoin;
            switch (action)
            {
                case "accept":
                    userJoin = eventRepository.GetUserJoinByUserIdAndEventId(uId, eId);
                    userJoin.Status = UserEventStatus.Accepted.ToString();
                    eventRepository.UpdateUserJoin(userJoin);
                    break;
                case "delete":
                    eventRepository.RemoveUserFromEvent(eId, uId); break;
                case "deny":
                    userJoin = eventRepository.GetUserJoinByUserIdAndEventId(uId, eId);
                    userJoin.Status = UserEventStatus.Denied.ToString();
                    eventRepository.UpdateUserJoin(userJoin);
                    break;
                case "present":
                    userJoin = eventRepository.GetUserJoinByUserIdAndEventId(uId, eId);
                    userJoin.Status = UserEventStatus.Presented.ToString();
                    eventRepository.UpdateUserJoin(userJoin);
                    break;
                case "absent":
                    userJoin = eventRepository.GetUserJoinByUserIdAndEventId(uId, eId);
                    userJoin.Status = UserEventStatus.Absent.ToString();
                    eventRepository.UpdateUserJoin(userJoin);
                    break;
                default:
                    return RedirectToPage("/Error");
            }

            return RedirectToPage("Detail", new { id = eId });
        }

        public IActionResult OnPostChangeEventStatus(string action, string eventId)
        {
            if (!int.TryParse(eventId, out int eId))
            {
                return RedirectToPage("/Error");
            }
            int? loginUserId = HttpContext.Session.GetInt32("UserId");
            if (loginUserId is null)
            {
                return RedirectToPage("/Dangptm/Login");
            }
            LoginUserId = (int)loginUserId;
            Event e = eventRepository.GetByEventId(eId);
            if (e is null)
            {
                return NotFound();
            }
            switch (action)
            {
                case "ongoing":
                    e.Status = EventStatus.OnGoing.ToString();
                    var users = eventRepository.GetUsersByEventId(eId);
                    foreach (var u in users)
                    {
                        UserJoin uj = eventRepository.GetUserJoinByUserIdAndEventId(u.UserId, e.EventId);
                        if (uj.Status.Equals(UserEventStatus.Pending.ToString()))
                        {
                            uj.Status = UserEventStatus.Absent.ToString();
                            eventRepository.UpdateUserJoin(uj);
                        }
                        else if (uj.UserId == loginUserId)
                        {
                            uj.Status = UserEventStatus.Presented.ToString();
                            eventRepository.UpdateUserJoin(uj);
                        }
                    }
                    break;
                case "ended":
                    e.Status = EventStatus.Ended.ToString(); break;
                case "suppend":
                    e.Status = EventStatus.Suppended.ToString(); break;
                case "continue":
                    e.Status = EventStatus.Waiting.ToString(); break;
                case "delete":
                    e.Status = EventStatus.Deleted.ToString();
                    eventRepository.Update(e);
                    return RedirectToPage("MyEvents");
                default:
                    return RedirectToPage("/Error");
            }
            eventRepository.Update(e);

            return RedirectToPage("Detail", new { id = eId });
        }
    }
}
