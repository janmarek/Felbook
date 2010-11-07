using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Felbook.Models;
using System.Web.Routing;

#region View models

namespace Felbook.Models
{
    public class MessageModelView
    {
        public DateTime Sent { get; set; }
        
        public bool Recieved { get; set; }

        public string SenderOrRecivers { get; set; }

        public string TextPreview { get; set; }

        public int indent { get; set; }
    }
}

#endregion

namespace Felbook.Controllers
{
    public class MessageController : Controller
    {

        #region Properties

        private Model model { get; set; }
        
        #endregion

        #region Init

        protected override void Initialize(RequestContext requestContext)
        {
            if (model == null)
            {
                model = new Model();
            }

            base.Initialize(requestContext);
        }

        #endregion

        #region Actions

        public ActionResult Index()
        {
            if ((User != null) && (Request.IsAuthenticated))
            {

                User user = model.UserService.FindByUsername(User.Identity.Name);
                List<Message> msgRootList = new List<Message>();
                List<Message> msgList = new List<Message>();

                foreach (var message in user.Messages)
                {
                    if (message.ReplyTo == null)
                    {
                        msgRootList.Add(message);
                    }
                }

                foreach (var message in user.SentMessages)
                {
                    if (message.ReplyTo == null)
                    {
                        msgRootList.Add(message);
                    }
                }
                msgRootList.Sort();

                CreateListForView(msgRootList, msgList);
                
                return View(model.UserService.FindByUsername(User.Identity.Name));
            }
            else
            {
                //return View("NotAuthorized");
                return View("Error");
            }
        }

        private void CreateListForView(List<Message> inputList, List<Message> outputList)
        {

            foreach (var message in inputList)
            {
                outputList.Add(message);

                if (message.Replies.Count != 0)
                {
                    CreateListForView(message.Replies.ToList(), outputList);
                }

            }
        }
        
        public ActionResult Sent()
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
            if ((User != null) && (Request.IsAuthenticated))
            {
                return View(model.MessageService.GetMessageById(msgID));
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
                User sender = model.UserService.FindByUsername(User.Identity.Name);
                
                char[] separators = new char[1];
                separators[0] = ' ';
                string[] parsedRecievers = collection["To"].Split(separators, StringSplitOptions.RemoveEmptyEntries);
                List<User> listOfRecievers = new List<User>();

                foreach (var reciever in parsedRecievers) 
                {
                    listOfRecievers.Add(model.UserService.FindByUsername(reciever));
                }

                Message prevMessage = model.MessageService.GetMessageById(int.Parse(collection["PrevMessageID"]));

                model.MessageService.SendMessageToUsers(sender, listOfRecievers, prevMessage, collection["text"]);
                return RedirectToAction("Sent");
            }
            catch (InvalidOperationException)
            {
                ModelState.AddModelError("", "User does not exist!");
                return View(collection);
            }

        }

        #endregion

    }
}


