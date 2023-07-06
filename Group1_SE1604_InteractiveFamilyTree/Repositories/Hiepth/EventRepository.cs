using BussinessObject.Models;
using DataAccesObject.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Hiepth
{
    public class EventRepository : IEventRepository
    {
        public void AddUsersToEvent(int eventId, List<int> userIds, int loggedInUserId) => EventDAO.Instance.AddUsersToEvent(eventId, userIds, loggedInUserId);

        public List<Event> GetAll() => EventDAO.Instance.GetEventList();

        public Event GetByEventId(int eventId) => EventDAO.Instance.GetByEventId(eventId);

        public List<Event> GetByFamilyId(int familyId) => EventDAO.Instance.GetFamilyEvents(familyId);

        public List<Event> GetByUserId(int userId) => EventDAO.Instance.GetByUserId(userId);

        public int GetUnseenEventCountByUserId(int userId) => EventDAO.Instance.GetUnseenEventCountByUserId(userId);

        public List<User> GetUsersByEventId(int eventId) => EventDAO.Instance.GetUsersByEventId(eventId);

        public void RemoveUserFromEvent(int eventId, int userId) => EventDAO.Instance.RemoveUserFromEvent(eventId, userId);

        public void RequestToJoinEvent(int userId, int eventId) => EventDAO.Instance.RequestToJoinEvent(userId, eventId);

        public int Save(Event entity) => EventDAO.Instance.AddEvent(entity);

        public void UpdateUserJoin(UserJoin userJoin) => EventDAO.Instance.UpdateUserJoin(userJoin);
        public UserJoin GetUserJoinByUserIdAndEventId(int userId, int eventId) => EventDAO.Instance.GetUserJoinByUserIdAndEventId(userId, eventId);

        public void Update(Event e) => EventDAO.Instance.Update(e);
    }
}
