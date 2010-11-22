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

        Message FindById(int ID);

        // Označí zprávu jako přečtenou daným uživatelem
        void MarkMessageReadBy(Message msg, User reader);

        // Označí zprávu jako nepřečtenou daným uživatelem
        void MarkMessageUnreadBy(Message msg, User reader);

        // Zjistí počet nepřečtených zpráv daného uživatele
        int NumberOfUnreadMessages(User user);
               
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
            msg.Readers.Add(sender);

            foreach (var reciever in recievers)
            {
                msg.Recievers.Add(reciever);
            }
                              
            db.MessageSet.AddObject(msg);
            db.SaveChanges();
        }


        public Message FindById(int ID)
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

        /// <summary>
        /// Označí zprávu jako přečtenou daným uživatelem
        /// </summary>
        /// <param name="msg">zpráva, která má být označena jako přečtená</param>
        /// <param name="reader">uživatel, který zprávu přečetl</param>
        public void MarkMessageReadBy(Message msg, User reader)
        {
            if(!msg.Readers.Contains(reader))
            {
                msg.Readers.Add(reader);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Označí zprávu jako nepřečtenou daným uživatelem
        /// </summary>
        /// <param name="msg">zpráva, která má být označena jako nepřečtená</param>
        /// <param name="reader">uživatel, který označil zprávu jako nepřečtenou</param>
        public void MarkMessageUnreadBy(Message msg, User reader)
        {
            if (msg.Readers.Contains(reader))
            {
                msg.Readers.Remove(reader);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Zjistí počet nepřečtených zpráv daného uživatele
        /// </summary>
        /// <param name="user">uživatel, jehož počet nepřečtených zpráv se bude zjišťovat</param>
        /// <returns>počet nepřečtených zpráv</returns>
        public int NumberOfUnreadMessages(User user)
        {
            int count = 0;

            foreach (var message in user.Messages)
            {
                if(user.ReadMessages.Contains(message))
                {
                    count++;
                }
            }

            return count;
        }

        #endregion

    }
}