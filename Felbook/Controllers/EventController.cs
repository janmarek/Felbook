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
        public IEnumerable<Event> Events { get; set; }
        public User CurrentUser { get; set; }
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
            return View(Model.EventService.FindEventById(id));
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
