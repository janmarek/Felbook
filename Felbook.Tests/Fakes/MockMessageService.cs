using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Felbook.Models;

namespace Felbook.Tests.Fakes
{
    class MockMessageService : IMessageService
    {
        public void SendMessageToUsers(User sender, ISet<User> recievers, Message prevMessage, string text)
        {
            throw new NotImplementedException();
        }

        public Message FindById(int ID)
        {
            throw new NotImplementedException();
        }

        public void MarkMessageReadBy(Message msg, User reader)
        {
            throw new NotImplementedException();
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
