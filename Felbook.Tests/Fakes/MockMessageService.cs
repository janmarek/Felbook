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
            throw new NotImplementedException();
        }

        public Message FindById(int ID)
        {
            return model.MessageList.Single(m => m.Id == ID);
        }

        public void MarkMessageReadBy(Message msg, User reader)
        {
            msg.Readers.Add(reader);
            reader.ReadMessages.Add(msg);
        }

        public void MarkMessageUnreadBy(Message msg, User reader)
        {
            throw new NotImplementedException();
        }

        public int NumberOfUnreadMessages(User user)
        {
            throw new NotImplementedException();
        }
    }
}
