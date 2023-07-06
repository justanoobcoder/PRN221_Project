
using BussinessObject.Models;
using DataAcessObject.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DataAccesObject.DAO
{
    public class EventDAO
    {
        private static EventDAO instance = null;
        private static object instanceLook = new object();

        public static EventDAO Instance
        {
            get
            {
                lock (instanceLook)
                {
                    if (instance == null)
                    {
                        instance = new EventDAO();
                    }
                    return instance;
                }
            }
        }
        FamilyTreeContext context = new FamilyTreeContext();
        public List<Event> GetEventList()
        {
            List<Event> events = null;

            try
            {
                // Get From Database
                events = context.Events
                    .Include(e => e.Creator)
                    .Where(e => !e.Status.Equals(EventStatus.Deleted.ToString()))
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return events;
        }

        public List<Event> GetFamilyEvents(int familyId)
        {
            List<Event> events = new List<Event>();
            try
            {
                List<User> users = context.Users
                    .Include(u => u.Events)
                    .Where(u => u.FamilyId == familyId && u.Password != null)
                    .ToList();
                users.ForEach(u => events.AddRange(u.Events.Where(e => !e.Status.Equals(EventStatus.Deleted.ToString()))));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return events;
        }

        public List<User> GetUsersByEventId(int eventId)
        {
            try
            {
                List<UserJoin> userJoins = context.Events
                    .Include(e => e.UserJoins)
                    .Where(e => e.EventId == eventId)
                    .Select(e => e.UserJoins).SingleOrDefault().ToList();
                List<User> users = new List<User>();
                userJoins.ForEach(u => users.Add(context.Users.Where(user => user.UserId == u.UserId).SingleOrDefault()));
                return users;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Event GetByEventId(int eventId)
        {
            try
            {
                return context.Events.Include(e => e.Creator).Where(e => e.EventId == eventId).SingleOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Event> GetByUserId(int userId)
        {
            try
            {
                List<Event> events = context.UserJoins
                    .Include(e => e.Event)
                    .Include(e => e.Event.Creator)
                    .Where(e => e.UserId == userId)
                    .Select(e => e.Event)
                    .Where(ev => !ev.Status.Equals(EventStatus.Deleted.ToString()))
                    .ToList();
                return events;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public UserJoin GetUserJoinByUserIdAndEventId(int userId, int eventId)
        {
            try
            {
                return context.UserJoins
                    .Where(u => u.UserId == userId && u.EventId == eventId)
                    .SingleOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int GetUnseenEventCountByUserId(int userId)
        {
            try
            {
                return context.UserJoins.Count(e => e.UserId == userId && e.View == 0);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void AddUsersToEvent(int eventId, List<int> userIds, int loggedInUserId)
        {
            try
            {
                foreach (int userId in userIds)
                {
                    UserJoin userJoin = new UserJoin
                    {
                        EventId = eventId,
                        UserId = userId,
                        Status = userId == loggedInUserId ? 
                            UserEventStatus.Accepted.ToString() :
                            UserEventStatus.Pending.ToString(),
                        View = userId == loggedInUserId ? 1 : 0
                    };
                    context.UserJoins.Add(userJoin);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void RequestToJoinEvent(int userId, int eventId)
        {
            try
            {
                UserJoin userJoin = new UserJoin
                {
                    EventId = eventId,
                    UserId = userId,
                    Status = UserEventStatus.Requested.ToString(),
                    View = 1
                };
                context.UserJoins.Add(userJoin);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void RemoveUserFromEvent(int eventId, int userId)
        {
            try
            {
                UserJoin userJoin = context.UserJoins.Where(e => e.EventId == eventId && e.UserId == userId).SingleOrDefault();
                context.UserJoins.Remove(userJoin);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateUserJoin(UserJoin userJoin)
        {
            try
            {
                context.Entry(userJoin).State = EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Event GetEvent(int eventId)
        {
            Event ev = new Event();

            try
            {
                ev = context.Events.SingleOrDefault(u => u.EventId == eventId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return ev;
        }

        public int AddEvent(Event ev)
        {
            if (ev == null)
            {
                throw new Exception("Event is undefined!!");
            }
            try
            {
                if (GetEvent(ev.EventId) == null)
                {
                    context.Events.Add(ev);
                    context.SaveChanges();
                    return ev.EventId;
                }
                else
                {
                    throw new Exception("Event is existed!!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(Event ev)
        {
            if (ev == null)
            {
                throw new Exception("Event is undefined!!");
            }
            try
            {
                Event e = GetByEventId(ev.EventId);
                if (e != null)
                {
                    context.Entry(e).State = EntityState.Detached;
                    context.Events.Update(ev);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Event does not exist!!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public void Delete(int eventId)
        {
            try
            {
                Event ev = GetEvent(eventId);
                if (ev != null)
                {
                    context.Events.Remove(ev);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Event does not exist!!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Event> searchEvent(string search)
        {
            List<Event> events = null;

            try
            {
                var filteredObjects = from obj in context.Events
                                      where obj.EventName.ToLower().Contains(search.ToLower())
                                      select obj;
                var result = filteredObjects.ToList();
                events = result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return events;
        }
    }
}


