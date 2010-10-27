using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Felbook.Models;
using System.Web.Routing;

namespace Felbook.Controllers
{
    public class MessageController : Controller
    {
        public FelBookDBEntities DbEntities { get; set; }

        protected override void Initialize(RequestContext requestContext)
        {
            if (DbEntities == null)
            {
                DbEntities = new FelBookDBEntities();
            }

            base.Initialize(requestContext);
        }

        public ActionResult Index(string username)
        {
            if ((User != null) && (username == User.Identity.Name))
            {
                User user = DbEntities.UserSet.Single(u => u.Username == username);
                return View(user);
            }
            else
            {
                //return View("NotAuthorized");
                return View("Error");
            }
        }

        public ActionResult Sent(string username)
        {
            if ((User != null) && (username == User.Identity.Name))
            {
                User user = DbEntities.UserSet.Single(u => u.Username == username);
                var msg = DbEntities.MessageSet.Where(m => m.Sender.Id == user.Id).ToList();
                return View(msg);
            }
            else
            {
                //return View("NotAuthorized");
                return View("Error");
            }
        }

        public ActionResult SendMessage(string username)
        {
            if ((User != null) && (username == User.Identity.Name))
            {
                User user = DbEntities.UserSet.Single(u => u.Username == username);
                return View(user);
            }
            else
            {
                //return View("NotAuthorized");
                return View("Error");
            }
        }

        [AcceptVerbs(HttpVerbs.Post), HttpPost]
        public ActionResult AddStatus(FormCollection collection)
        {
            return RedirectToAction("Index", new { username = User.Identity.Name });
        }

    }
}


