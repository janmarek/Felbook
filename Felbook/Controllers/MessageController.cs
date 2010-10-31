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

        public ActionResult Index()
        {
            if ((User != null) && (Request.IsAuthenticated))
            {
                return View(model.UserService.FindByUsername(User.Identity.Name));
            }
            else
            {
                //return View("NotAuthorized");
                return View("Error");
            }
        }

        public ActionResult Sent()
        {
            if ((User != null) && (Request.IsAuthenticated))
            {
                return View(msgModel.GetMessagesSentByUser(User.Identity.Name));
            }
            else
            {
                //return View("NotAuthorized");
                return View("Error");
            }
        }

        public ActionResult SendMessage()
        {
            if ((User != null) && (Request.IsAuthenticated))
            {
                return View(model.UserService.FindByUsername(User.Identity.Name));
            }
            else
            {
                //return View("NotAuthorized");
                return View("Error");
            }
        }

        public ActionResult ReplyMessage(int msgID)
        {
            if ((User != null) && (Request.IsAuthenticated) /*&& (msgID != null)*/)
            {
                return View(msgModel.GetMessageById(msgID));
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

                msgModel.SendMessageToUsers(User.Identity.Name, listOfRecievers, int.Parse(collection["PrevMessageID"]), collection["text"]);
                return RedirectToAction("Sent");
            }
            catch (InvalidOperationException)
            {
                ModelState.AddModelError("", "User does not exist!");
                return View(collection);
            }
            
        }

        
    }
}


