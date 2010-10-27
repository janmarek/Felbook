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
        List<Message> getMessagesSentByUser(string username);

        void sendMessageToUsers(string sender, List<string> recievers, string text);
    }
    
    public class MessageModel : IMessageModel
    {

        public FelBookDBEntities DbEntities { get; set; }

        public MessageModel()
        {
            DbEntities = new FelBookDBEntities();
        }

        public List<Message> getMessagesSentByUser(string username)
        {
            User user = DbEntities.UserSet.Single(u => u.Username == username);
            return DbEntities.MessageSet.Where(m => m.Sender.Id == user.Id).ToList();
        }

        public void sendMessageToUsers(string sender, List<string> recievers, string text)
        {
            Message msg = new Message();
            msg.Created = DateTime.Now;
            msg.Sender = DbEntities.UserSet.Single(u => u.Username == sender);
            msg.Text = text;
            foreach (var reciever in recievers)
            {
                msg.Users.Add(DbEntities.UserSet.Single(u => u.Username == reciever));
            }
            
            DbEntities.MessageSet.AddObject(msg);
            DbEntities.SaveChanges();
        }

    }
}