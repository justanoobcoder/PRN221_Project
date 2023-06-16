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
        void addUsersToEvent(int eventId, List<int> userIds);
        void requestToJoinEvent(int userId, int eventId);
        void removeUserFromEvent(int eventId, int userId);
        void updateUserJoin(UserJoin userJoin);
    }
}
