using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Felbook.Models;

namespace Felbook.Tests.Fakes
{
    class MockMessageService : AbstractMockService, IMessageService
    {

        public MockMessageService(MockModel model) : base(model) { }
        
        public void SendMessageToUsers(User sender, ISet<User> recievers, Message prevMessage, string text)
        {

            Message msg = new Message();
            msg.Created = DateTime.Now;
            msg.Sender = sender;
            msg.ReplyTo = prevMessage;
            msg.Text = text;
            
            foreach (var reciever in recievers)
            {
                msg.Recievers.Add(reciever);
            }

            model.MessageList.Add(msg);
            
        }

        public Message FindById(int ID)
        {
            try
            {
                return model.MessageList.Single(m => m.Id == ID);
            }
            catch (InvalidOperationException)
            {
                return null;
            }
        }

        public void MarkMessageReadBy(Message msg, User reader)
        {
            msg.Readers.Add(reader);
            reader.ReadMessages.Add(msg);
        }

        public void MarkMessageUnreadBy(Message msg, User reader)
        {
            msg.Readers.Remove(reader);
            reader.ReadMessages.Remove(msg);
        }

        public int NumberOfUnreadMessages(User user)
        {
            throw new NotImplementedException();
        }
    }
}
