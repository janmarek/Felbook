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
        public ISystemService service { get; set; }
        public IMessageModel model { get; set; }

        protected override void Initialize(RequestContext requestContext)
        {
            if (service == null)
            {
                service = new SystemService();
            }

            if (model == null)
            {
                model = new MessageModel();
            }

            base.Initialize(requestContext);
        }

        public ActionResult Index(string username)
        {
            if ((User != null) && (username == User.Identity.Name))
            {
                //User user = DbEntities.UserSet.Single(u => u.Username == username);
                User user = service.SearchUsers(username).Single();
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
                return View(model.getMessagesSentByUser(username));
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
                //User user = DbEntities.UserSet.Single(u => u.Username == username);
                User user = service.SearchUsers(username).Single();
                return View(user);
            }
            else
            {
                //return View("NotAuthorized");
                return View("Error");
            }
        }

        [AcceptVerbs(HttpVerbs.Post), HttpPost]
        public ActionResult SendMessage(FormCollection collection)
        {
            try
            {
                //List<string> listOfRecievers = new List<string>();
                string recievers = collection["To"];
                string[] separators = new string[1];
                separators[0] = " ";
                List<string> listOfRecievers = recievers.Split(separators, StringSplitOptions.RemoveEmptyEntries).ToList();

                //listOfRecievers.Add(collection["To"]);
                model.sendMessageToUsers(User.Identity.Name, listOfRecievers, collection["text"]);
                return RedirectToAction("Sent", new { username = User.Identity.Name });
            }
            catch (InvalidOperationException)
            {
                ModelState.AddModelError("", "User does not exist!");
                return View(collection);
            }
            
        }

    }
}


