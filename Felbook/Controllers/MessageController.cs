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
        private Model model { get; set; }
        private IMessageModel msgModel { get; set; }

        protected override void Initialize(RequestContext requestContext)
        {
            if (model == null)
            {
                model = new Model();
            }

            if (msgModel == null)
            {
                msgModel = new MessageModel();
            }

            base.Initialize(requestContext);
        }

        public ActionResult Index(string username)
        {
            if ((User != null) && (username == User.Identity.Name))
            {
                return View(model.UserService.GetByUsername(username));
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
                return View(msgModel.getMessagesSentByUser(username));
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
                return View(model.UserService.GetByUsername(username));
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
                string recievers = collection["To"];
                string[] separators = new string[1];
                separators[0] = " ";
                
                List<string> listOfRecievers = recievers.Split(separators, StringSplitOptions.RemoveEmptyEntries).ToList();

                msgModel.sendMessageToUsers(User.Identity.Name, listOfRecievers, collection["text"]);
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


