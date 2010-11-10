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
        public int ID { get; set; }
        
        public DateTime Sent { get; set; }
        
        public bool Recieved { get; set; }

        public string SenderOrRecivers { get; set; }

        public string TextPreview { get; set; }

        public int Indent { get; set; }
    }

    public class MessageListView
    {
        public List<MessageModelView> MessageList { get; set; }

        public int LastPage { get; set; }

        public int ActualPage { get; set; }
    }

    public class SendMessageView
    {
        public string AutocompleteUsers { get; set; }

        public string AutocompleteGroups { get; set; }
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

        /// <summary>
        /// Zobrazit zprávy seznam zpráv
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(int page)
        {
                        
            if ((User != null) && (Request.IsAuthenticated))
            {

                User user = model.UserService.FindByUsername(User.Identity.Name);
                
                List<Message> msgRootList = new List<Message>();
                List<MessageModelView> msgList = new List<MessageModelView>();
                List<MessageModelView> pageList;
                
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
                CreateListForView(msgRootList, msgList, 0);

                if (msgList.Count < (10 * page))
                {
                    int countOfMessages = msgList.Count - (10 * page) + 10;

                    if (countOfMessages > 0)
                    {
                        pageList = msgList.GetRange(10 * (page - 1), countOfMessages);
                    }
                    else
                    {
                        //return View("NotExist");
                        return View("Error");
                    }
                }
                else
                {
                    pageList = msgList.GetRange(10 * (page - 1), 10);
                }

                return View(new MessageListView
                {
                    MessageList = pageList,
                    LastPage = msgList.Count <= 10 ? 1 : (msgList.Count - 1) / 10 + 1,
                    ActualPage = page
                });
            }
            else
            {
                //return View("NotAuthorized");
                return View("Error");
            }
        }

        private void CreateListForView(List<Message> inputList, List<MessageModelView> outputList, int indent)
        {

            foreach (var message in inputList)
            {
                MessageModelView messageView = new MessageModelView();
                messageView.ID = message.Id;
                messageView.Sent = message.Created;

                if (message.Sender.Username == User.Identity.Name)
                {
                    messageView.Recieved = false;
                    IEnumerator<User> iterator = message.Users.GetEnumerator();

                    if (!iterator.MoveNext())
                    {
                        continue;  // žadný příjemce -> chybná zpráva 
                    }

                    messageView.SenderOrRecivers = iterator.Current.Username;

                    while (iterator.MoveNext())
                    {
                        messageView.SenderOrRecivers += ", ";
                        messageView.SenderOrRecivers += iterator.Current.Username;
                    }
                }
                else
                {

                    if (!message.Users.Contains(model.UserService.FindByUsername(User.Identity.Name)))
                    {
                        continue; // pokud mi zpráva nepatří, tak ji zahodím
                    }

                    messageView.Recieved = true;
                    messageView.SenderOrRecivers = message.Sender.Username;
                }

                if (message.Text.Length <= 50)
                {
                    messageView.TextPreview = message.Text;
                }
                else
                {
                    messageView.TextPreview = message.Text.Substring(0, 50);
                    messageView.TextPreview += "...";
                }

                messageView.Indent = indent;

                outputList.Add(messageView);

                if (message.Replies.Count != 0)
                {
                    CreateListForView(message.Replies.ToList(), outputList, indent + 20);
                }

            }
        }

        /// <summary>
        /// Zobrazit detaily zprávy
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        public ActionResult Detail(int id)
        {
            return View(model.MessageService.GetMessageById(id));
        }

        /// <summary>
        /// Poslat zprávu
        /// </summary>
        /// <returns></returns>
        public ActionResult SendMessage()
        {
            if ((User != null) && (Request.IsAuthenticated))
            {
                // TODO domluvit se, co má být v autocomplete seznamu
                
                FelBookDBEntities db = new FelBookDBEntities();

                string users = "";
                string groups = "";

                IEnumerator<User> userEnum = db.UserSet.AsEnumerable().GetEnumerator();

                if (userEnum.MoveNext())
                {
                    users += "\"" + userEnum.Current.Username + "\"";
                }
                while (userEnum.MoveNext())
                {
                    users += ", \"" + userEnum.Current.Username + "\"";
                }

                IEnumerator<Group> groupEnum = db.GroupSet.AsEnumerable().GetEnumerator();

                if (groupEnum.MoveNext())
                {
                    groups += "\"" + groupEnum.Current.Name + "\"";
                }
                while (groupEnum.MoveNext())
                {
                    groups += ", \"" + groupEnum.Current.Name + "\"";
                }

                return View(new SendMessageView
                {
                    AutocompleteUsers = users,
                    AutocompleteGroups = groups
                });
                
                //return View(model.UserService.FindByUsername(User.Identity.Name));
            }
            else
            {
                //return View("NotAuthorized");
                return View("Error");
            }
        }

        /// <summary>
        /// Odpovědět na zprávu
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
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
                ISet<User> setOfRecievers = new HashSet<User>();

                if (String.IsNullOrEmpty(collection["text"]))
                {
                    ModelState.AddModelError("", "Text of message is requied!");
                    return View(collection);
                }

                for (int i = 1; i <= int.Parse(collection["UserCounter"]); i++)
                {
                    string reciever = collection["ToUser" + i];
                    if (String.IsNullOrEmpty(reciever) || (reciever == sender.Username))
                    {
                        continue;
                    }
                    setOfRecievers.Add(model.UserService.FindByUsername(reciever));
                }

                for (int i = 1; i <= int.Parse(collection["GroupCounter"]); i++)
                {
                    string groupName = collection["ToGroup" + i];
                    if (String.IsNullOrEmpty(groupName))
                    {
                        continue;
                    }
                    Group group = model.GroupService.SearchGroups(groupName).Single(g => g.Name == groupName);
                    foreach( var user in model.GroupService.GetUsers(group))
                    {
                        if (user.Username == sender.Username)
                        {
                            continue;
                        }
                        setOfRecievers.Add(user);
                    }
                }

                if (setOfRecievers.Count == 0)
                {
                    ModelState.AddModelError("", "No user selected!");
                    return View(collection);
                }

                Message prevMessage = model.MessageService.GetMessageById(int.Parse(collection["PrevMessageID"]));

                model.MessageService.SendMessageToUsers(sender, setOfRecievers, prevMessage, collection["text"]);
                return RedirectToAction("Index", new { page = 1.ToString() });
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


