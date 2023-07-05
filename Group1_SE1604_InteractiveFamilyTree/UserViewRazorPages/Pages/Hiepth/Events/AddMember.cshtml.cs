using BussinessObject.Models;
using DataAcessObject.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPage.ViewModels;
using Repositories.Hiepth;
using System.Collections.Generic;
using System.Linq;

namespace UserViewRazorPages.Pages.Hiepth.Events
{
    [BindProperties]
    public class AddMemberModel : PageModel
    {
        private readonly IEventRepository eventRepository = new EventRepository();
        private readonly IUserRepository userRepository = new UserRepository();

        private const string AddedUsersSession = "EventAddedUsers";

        public Event Event { get; set; }
        public List<User> Users { get; set; }
        public List<User> AddedUsers { get; set; }

        public IActionResult OnGet(int eventId)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId is null)
            {
                return RedirectToPage("/Dangptm/Login");
            }
            AddedUsers = SessionHelper.GetObjectFromJson<List<User>>(HttpContext.Session, AddedUsersSession);
            if (AddedUsers is null)
            {
                AddedUsers = new List<User>();
            }
            Event = eventRepository.GetByEventId(eventId);
            int familyId = (int)Event.Creator.FamilyId;
            Users = userRepository.GetUsersHaveAccountByFamilyId(familyId).Except(AddedUsers, new UserComparer()).ToList();

            return Page();
        }

        public IActionResult OnPostAddMember(string eventId, string userId)
        {
            int? loggedInUserId = HttpContext.Session.GetInt32("UserId");
            if (loggedInUserId is null)
            {
                return RedirectToPage("/Dangptm/Login");
            }
            AddedUsers = SessionHelper.GetObjectFromJson<List<User>>(HttpContext.Session, AddedUsersSession);
            if (AddedUsers is null)
            {
                AddedUsers = new List<User>();
            }
            AddedUsers.Add(userRepository.GetById(int.Parse(userId)));
            SessionHelper.SetObjectAsJson(HttpContext.Session, AddedUsersSession, AddedUsers);

            return RedirectToPage("AddMember", new { eventId = int.Parse(eventId) } );
        }

        public IActionResult OnPostDeleteMember(string eventId, string index)
        {
            int? loggedInUserId = HttpContext.Session.GetInt32("UserId");
            if (loggedInUserId is null)
            {
                return RedirectToPage("/Dangptm/Login");
            }
            AddedUsers = SessionHelper.GetObjectFromJson<List<User>>(HttpContext.Session, AddedUsersSession);
            if (AddedUsers is null || AddedUsers.Count == 0)
            {
                return NotFound();
            }
            AddedUsers.RemoveAt(int.Parse(index));
            SessionHelper.SetObjectAsJson(HttpContext.Session, AddedUsersSession, AddedUsers);

            return RedirectToPage("AddMember", new { eventId = int.Parse(eventId) });
        }

        public IActionResult OnPost(string eventId)
        {
            int? loggedInUserId = HttpContext.Session.GetInt32("UserId");
            if (loggedInUserId is null)
            {
                return RedirectToPage("/Dangptm/Login");
            }
            AddedUsers = SessionHelper.GetObjectFromJson<List<User>>(HttpContext.Session, AddedUsersSession);

            if (AddedUsers is null || AddedUsers.Count == 0)
            {
                return NotFound();
            }
            List<int> userIds = AddedUsers.Select(x => x.UserId).ToList();
            eventRepository.AddUsersToEvent(int.Parse(eventId), userIds);
            return RedirectToPage("MyFamilyEvents");
        }
    }
}
