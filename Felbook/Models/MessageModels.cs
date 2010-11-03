using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Felbook.Models
{

    public interface IMessageModel
    {
        
        void SendMessageToUsers(string sender, List<string> recievers, int prevMessageID, string text);

        Message GetMessageById(int ID);
               
    }

    public class MessageModel : IMessageModel
    {

        #region Properities

        private FelBookDBEntities DbEntities { get; set; }

        private Model model { get; set; }

        #endregion

        #region Construcotrs

        public MessageModel()
        {
            DbEntities = new FelBookDBEntities();
            model = new Model();
        }

        #endregion

        #region Methods

        public void SendMessageToUsers(string sender, List<string> recievers, int prevMessageID ,string text)
        {
            Message msg = new Message();
            msg.Created = DateTime.Now;
            //msg.Sender = model.UserService.FindByUsername(sender);
            msg.Sender = DbEntities.UserSet.Single(u => u.Username == sender);

            if (prevMessageID == 0)
            {
                msg.FirstMessage = null;
            }
            else
            {
                msg.FirstMessage = GetMessageById(prevMessageID);
            }

            msg.Text = text;
            foreach (var reciever in recievers)
            {
                //msg.Users.Add(model.UserService.FindByUsername(reciever));
                msg.Users.Add(DbEntities.UserSet.Single(u => u.Username == reciever));
            }
                              
            DbEntities.MessageSet.AddObject(msg);
            DbEntities.SaveChanges();
        }

        public Message GetMessageById(int ID)
        {
            try
            {
                return DbEntities.MessageSet.Single(m => m.Id == ID);
            }
            catch (InvalidOperationException)
            {
                return null;
            }
        }

        #endregion

    }
}