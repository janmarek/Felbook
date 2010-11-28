﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Felbook.Models;


#region View models

namespace Felbook.Models
{
    public class MessageModelView
    {
        public int ID { get; set; }
        
        public DateTime Sent { get; set; }
        
        public bool Recieved { get; set; }

        public bool Read { get; set; }

        public string SenderOrRecivers { get; set; }

        public string TextPreview { get; set; }

        public int Indent { get; set; }
    }

    public class MessageListView
    {
        public List<MessageModelView> MessageList { get; set; }

        public int LastPage { get; set; }

        public int ActualPage { get; set; }

        //public int UnreadCount { get; set; }
    }

    public class SendMessageModel
    {
        public string AutocompleteUsers { get; set; }

        public string AutocompleteGroups { get; set; }

        public Message prevMessage { get; set; }

        [Required(ErrorMessage = "Text of message is requied!")]
        [DisplayName("Text:")]
        public string Text { get; set; }
                
    }
}

#endregion

namespace Felbook.Controllers
{
    public class MessageController : FelbookController
    {

        #region Constructors

        public MessageController(IModel model) : base(model) {}
        public MessageController() {}

        #endregion

        #region Actions

        #region MessageList

        /// <summary>
        /// Zobrazit zprávy seznam zpráv
        /// </summary>
        /// <param name="page">page</param>
        /// <returns></returns>
        [Authorize]
        public ActionResult Index(int page)
        {
            if (page < 1)
            {
                return View("NotExist");
            }

            List<Message> msgRootList = new List<Message>();
            List<MessageModelView> msgList = new List<MessageModelView>();
            List<MessageModelView> pageList;
                
            foreach (var message in CurrentUser.Messages)
            {
                if (message.ReplyTo == null)
                {
                    msgRootList.Add(message);
                }
            }

			foreach (var message in CurrentUser.SentMessages)
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
                    return View("NotExist");
                    //return View("Error");
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
                ActualPage = page//,
                //UnreadCount = Model.MessageService.NumberOfUnreadMessages(CurrentUser)
            });
            
        }

        /// <summary>
        /// Pomocná metoda pro zobrazení seznamu zpráv
        /// </summary>
        /// <param name="inputList">inputList</param>
        /// <param name="outputList">outputList</param>
        /// <param name="indent">indent</param>
        /// <returns></returns>
        private void CreateListForView(List<Message> inputList, List<MessageModelView> outputList, int indent)
        {

            foreach (var message in inputList)
            {
                MessageModelView messageView = new MessageModelView();
                messageView.ID = message.Id;
                messageView.Sent = message.Created;

                if (message.Sender.Username == CurrentUser.Username)
                {
                    messageView.Recieved = false;
                    IEnumerator<User> iterator = message.Recievers.GetEnumerator();

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

                    if (!message.Recievers.Contains(CurrentUser))
                    {
                        continue; // pokud mi zpráva nepatří, tak ji zahodím
                    }

                    messageView.Recieved = true;
                    messageView.SenderOrRecivers = message.Sender.Username;
                }

                messageView.Read = message.Readers.Contains(CurrentUser);
                
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

        #endregion

        #region MessageDetail

        /// <summary>
        /// Zobrazit detaily zprávy
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        [Authorize]
        public ActionResult Detail(int id)
        {
            Message msg = Model.MessageService.FindById(id);
            
            if (msg != null && (msg.Sender == CurrentUser || msg.Recievers.Contains(CurrentUser)))
            {
                Model.MessageService.MarkMessageReadBy(msg, CurrentUser);
                return View(msg);
            }
            else
            {
                return View("NotAuthorized");
                //return View("Error");
            }
        }

        [Authorize]
        public ActionResult UnreadMessage(int msgid)
        {
            Message msg = Model.MessageService.FindById(msgid);

            if (msg != null)
            {
                Model.MessageService.MarkMessageUnreadBy(msg, CurrentUser);
            }
            
            return RedirectToAction("Index", new { page = 1.ToString() });
        }

        #endregion

        #region SendMessage

        /// <summary>
        /// Poslat zprávu
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult SendMessage()
        {
            return View(prepareMessageModelToSend());    
        }

        /// <summary>
        /// Pomocná metoda, která připravuje seznam uživatelů skupina pro autocomplete funkc.
        /// </summary>
        /// <returns>Zobrazovací model s připravenými seznami</returns>
        private SendMessageModel prepareMessageModelToSend()
        {
            string users = "";
            string groups = "";

            IEnumerator<User> userEnum = CurrentUser.Followings.AsEnumerable().GetEnumerator();

            if (userEnum.MoveNext())
            {
                users += "\"" + userEnum.Current.Username + "\"";
            }
            while (userEnum.MoveNext())
            {
                users += ", \"" + userEnum.Current.Username + "\"";
            }

            IEnumerator<Group> groupEnum = CurrentUser.JoinedGroups.AsEnumerable().GetEnumerator();

            if (groupEnum.MoveNext())
            {
                groups += "\"" + groupEnum.Current.Name + "\"";
            }
            while (groupEnum.MoveNext())
            {
                groups += ", \"" + groupEnum.Current.Name + "\"";
            }

            return new SendMessageModel
            {
                AutocompleteUsers = users,
                AutocompleteGroups = groups
            };
        }

        #endregion

        #region ReplyMessage

        /// <summary>
        /// Odpovědět na zprávu
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        [Authorize]
        public ActionResult ReplyMessage(int msgID)
        {
            Message msg = Model.MessageService.FindById(msgID);

            if (msg != null && (msg.Recievers.Contains(CurrentUser)))
            {
                return View(prepareMessageModelToReply(msg));
            }
            else
            {
                return View("NotAuthorized");
                //return View("Error");
            }
        }

        private SendMessageModel prepareMessageModelToReply(Message msg)
        {
            return new SendMessageModel
            {
                prevMessage = msg
            };
        }

        #endregion

        [AcceptVerbs(HttpVerbs.Post), HttpPost]
        public ActionResult SendMessage(SendMessageModel model, FormCollection collection)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    User sender = CurrentUser;
                    ISet<User> setOfRecievers = new HashSet<User>();
                    string[] separators = new string[1];
                    separators[0] = "; ";

                    string[] parsedRecievers = collection["ToUsers"].Split(separators, StringSplitOptions.RemoveEmptyEntries);

                    foreach (var reciever in parsedRecievers)
                    {
                        if (String.IsNullOrEmpty(reciever) || (reciever == sender.Username))
                        {
                            continue;
                        }
                        setOfRecievers.Add(Model.UserService.FindByUsername(reciever));
                    }

                    string[] parsedGroups = collection["ToGroups"].Split(separators, StringSplitOptions.RemoveEmptyEntries);

                    foreach (var groupName in parsedGroups)
                    {
                        if (String.IsNullOrEmpty(groupName))
                        {
                            continue;
                        }
                        Group group = Model.GroupService.FindByName(groupName);
                        foreach (var user in Model.GroupService.GetUsers(group))
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

                        var newMsgModel = prepareMessageModelToSend();
                        newMsgModel.Text = model.Text;

                        return View(newMsgModel);
                    }

                    Message prevMessage = Model.MessageService.FindById(int.Parse(collection["PrevMessageID"]));

                    Model.MessageService.SendMessageToUsers(sender, setOfRecievers, prevMessage, model.Text);
                    return RedirectToAction("Index", new { page = 1.ToString() });
                }
                catch (InvalidOperationException)
                {
                    ModelState.AddModelError("", "User does not exist!");
                    
                    var newMsgModel = prepareMessageModelToSend();
                    newMsgModel.Text = model.Text;

                    return View(newMsgModel);
                }
            }
            else
            {
                if (int.Parse(collection["PrevMessageID"]) == -1)
                {
                    var newMsgModel = prepareMessageModelToSend();
                    newMsgModel.Text = model.Text;

                    return View("SendMessage", newMsgModel);
                }
                else
                {
                    var newMsgModel = prepareMessageModelToReply(Model.MessageService.FindById(int.Parse(collection["PrevMessageID"])));
                    newMsgModel.Text = model.Text;

                    return View("ReplyMessage", newMsgModel);
                }
            }
 
        }

        #endregion

    }
}


