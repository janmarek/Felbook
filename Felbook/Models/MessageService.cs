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
        
        void SendMessageToUsers(string sender, List<string> recievers, int prevMessageID, string text);

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
            model = new Model();
        }

        #endregion

        #region Methods

        public void SendMessageToUsers(string sender, List<string> recievers, int prevMessageID ,string text)
        {
            Message msg = new Message();
            msg.Created = DateTime.Now;
            //msg.Sender = model.UserService.FindByUsername(sender);
            msg.Sender = db.UserSet.Single(u => u.Username == sender);

            if (prevMessageID == 0)
            {
                msg.ReplyTo = null;
            }
            else
            {
				msg.ReplyTo = GetMessageById(prevMessageID);
            }

            msg.Text = text;
            foreach (var reciever in recievers)
            {
                //msg.Users.Add(model.UserService.FindByUsername(reciever));
                msg.Users.Add(db.UserSet.Single(u => u.Username == reciever));
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