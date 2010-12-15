using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Felbook.Models;
using Felbook.Helpers;

#region ViewModels

namespace Felbook.Models
{

    public class EventViewModel
    {
        public IEnumerable<Status> Status { get; set; }
        public User CurrentUser { get; set; }
        public Group Group { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public String Name { get; set; }
        public String Text { get; set; }

        public ImageOutputHelper ImageOutput { get; set; }
        public FileOutputHelper FileOutput { get; set; }

        public StatusViewModel CreateStatusViewModel(Status status)
        {
            return new StatusViewModel { Status = status, FileOutput = FileOutput, ImageOutput = ImageOutput };
        }
    }
}

#endregion

namespace Felbook.Controllers
{
    public class EventController : FelbookController
    {
        public EventViewModel CreateEventViewModel(IEnumerable<Status> status, User user, Group group, DateTime from, DateTime To, String name, String text) {
            return new EventViewModel { Status = status, CurrentUser = user, Group = group, From = from, To = To, Name = name, Text = text, FileOutput = new FileOutputHelper(Model.FileService), ImageOutput = new ImageOutputHelper(Model.ImageService) };
        }
        //
        // GET: /Event/
        [Authorize]
        public ActionResult Index()
        {
            return View(Model.EventService.GetEvents(CurrentUser));
        }

        //
        // GET: /Event/Details/5

        public ActionResult Details(int id)
        {
            Event ev = Model.EventService.FindEventById(id);
            return View(CreateEventViewModel(ev.Status,ev.User,ev.Group,ev.From,ev.To,ev.Name,ev.Text));
        }

        //
        // GET: /Event/Create
        [Authorize]
        public ActionResult Create()
        {            
            return View();
        } 

        //
        // POST: /Event/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
                IQueryable<Group> groups;
                String grupa = collection["Group"];
                Group group = null;
                groups = Model.GroupService.SearchGroups(grupa);
                if(!String.IsNullOrEmpty(grupa)) group = groups.Single(m => m.Name == grupa);


                DateTime from = Convert.ToDateTime(Request.Form["From"]);
                DateTime to = Convert.ToDateTime(Request.Form["To"]);
                String name = Request.Form["Name"];
                String text = Request.Form["Text"];

                
                Model.EventService.AddEvent(CurrentUser, group, from, to, name, text);

                return RedirectToAction("Index");
            
            
        }
        

        //
        // GET: /Event/Delete/5
        [Authorize]
        public ActionResult Delete(int id)
        {
            Event ev = Model.EventService.FindEventById(id);
            return View(ev);
        }

        //
        // POST: /Event/Delete/5

        [HttpPost]
        public ActionResult Delete(FormCollection collection)
        {
            //try
            //{
                Model.EventService.DeleteEvent(int.Parse(collection["EventID"]));
                return RedirectToAction("Index");
            //}
            //catch
            //{
            //    return View();
            //}
        }
    }
}
