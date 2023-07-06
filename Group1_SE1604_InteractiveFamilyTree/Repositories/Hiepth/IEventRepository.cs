using BussinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Hiepth
{
    public interface IEventRepository
    {
        List<Event> GetAll();
        List<Event> GetByFamilyId(int familyId);
        List<Event> GetByUserId(int userId);
        Event GetByEventId(int eventId);
        int Save(Event entity);
        int GetUnseenEventCountByUserId(int userId);
        void AddUsersToEvent(int eventId, List<int> userIds, int loggedInUserId);
        void RequestToJoinEvent(int userId, int eventId);
        void RemoveUserFromEvent(int eventId, int userId);
        void Update(Event e);
        void UpdateUserJoin(UserJoin userJoin);
        List<User> GetUsersByEventId(int eventId);
        UserJoin GetUserJoinByUserIdAndEventId(int userId, int eventId);
    }
}
