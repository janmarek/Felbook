using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Felbook.Models
{
    public interface IEventService
    {
        void AddEvent(User user, Group group, DateTime from, DateTime to, String name, String text);
        Event FindEventById(int id);
        IEnumerable<Event> GetEvents(User user);
        void DeleteEvent(int id);
    }

    public class EventService : IEventService
    {
        #region Proměnné

        private FelBookDBEntities db;

        #endregion


        #region Konstruktor

		public EventService(FelBookDBEntities DBEntities)
		{
			db = DBEntities;			
		}

		#endregion

        #region Metody

        public void AddEvent(User user, Group group, DateTime from, DateTime to, String name, String text)
        {
            Event ev = new Event();
            ev.Group = group;
            ev.User = user;
            ev.From = from;
            ev.To = to;
            ev.Name = name;
            ev.Text = text;

            db.AddToEventSet(ev);
            
            db.SaveChanges();
        }

        public Event FindEventById(int id)
        {
            try
            {
                return db.EventSet.Single(m => m.Id == id);
            }
            catch (InvalidOperationException)
            {
                return null;
            }
        }

        public IEnumerable<Event> GetEvents(User user)
        {
            return user.EventsCreated.OrderByDescending(w => w.Id).ToArray();
        }

        public void DeleteEvent(int id) 
        {
            Event ev = FindEventById(id);
            foreach (var stat in ev.Status.ToList())
            {
                if(stat != null)
                {
                    db.StatusSet.DeleteObject(stat);
                }
            }
            db.EventSet.DeleteObject(ev);

            db.SaveChanges();
        }

        #endregion

        #region EventFormModel
        public class EventFormModel
        {
            public string Text { get; set; }
            public string Name { get; set; }
            public DateTime From { get; set; }
            public DateTime To { get; set; }
        }
        #endregion
    }
}