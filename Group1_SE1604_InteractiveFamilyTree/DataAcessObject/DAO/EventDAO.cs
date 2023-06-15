
using BussinessObject.Models;
using DataAcessObject.Bodt;
using Microsoft.EntityFrameworkCore;
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
                events = context.Events.ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return events;
        }

        public List<Event> GetFamilyEvents(int familyId)
        {
            List<Event> events = null;
            try
            {
                events = context.Events.Where(od => UserDAO.Instance.getFamilyId(od.CreatorId.GetValueOrDefault()) == familyId).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return events;
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

        public void AddEvent(Event ev)
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
                Event e = GetEvent(ev.EventId);
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


