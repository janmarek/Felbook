using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Felbook.Models
{

    public interface IMessageService
    {

        void SendMessageToUsers(User sender, ISet<User> recievers, Message prevMessage, string text);

        Message GetMessageById(int ID);
               
    }

    public class MessageService : IMessageService
    {

        #region Properities

        private FelBookDBEntities db { get; set; }

        private Model model { get; set; }

        #endregion

        #region Construcotrs

        public MessageService(FelBookDBEntities DBEntities)
        {
            db = DBEntities;
        }

        #endregion

        #region Methods

        public void SendMessageToUsers(User sender, ISet<User> recievers, Message prevMessage, string text)
        {
            Message msg = new Message();
            msg.Created = DateTime.Now;
            msg.Sender = sender;
            msg.ReplyTo = prevMessage;
            msg.Text = text;

            foreach (var reciever in recievers)
            {
                msg.Users.Add(reciever);
            }
                              
            db.MessageSet.AddObject(msg);
            db.SaveChanges();
        }


        public Message GetMessageById(int ID)
        {
            try
            {
                return db.MessageSet.Single(m => m.Id == ID);
            }
            catch (InvalidOperationException)
            {
                return null;
            }
        }

        #endregion

    }
}