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
        public void addUsersToEvent(int eventId, List<int> userIds) => EventDAO.Instance.addUsersToEvent(eventId, userIds);

        public List<Event> GetAll() => EventDAO.Instance.GetEventList();

        public Event GetByEventId(int eventId) => EventDAO.Instance.GetByEventId(eventId);

        public List<Event> GetByFamilyId(int familyId) => EventDAO.Instance.GetFamilyEvents(familyId);

        public List<Event> GetByUserId(int userId) => EventDAO.Instance.GetByUserId(userId);

        public int GetUnseenEventCountByUserId(int userId) => EventDAO.Instance.GetUnseenEventCountByUserId(userId);

        public void removeUserFromEvent(int eventId, int userId) => EventDAO.Instance.removeUserFromEvent(eventId, userId);

        public void requestToJoinEvent(int userId, int eventId) => EventDAO.Instance.requestToJoinEvent(userId, eventId);

        public int Save(Event entity) => EventDAO.Instance.AddEvent(entity);

        public void updateUserJoin(UserJoin userJoin) => EventDAO.Instance.updateUserJoin(userJoin);
    }
}
