using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Felbook.Models;

namespace Felbook.Tests.Fakes
{
    class MockUserService : IUserService
    {

        public IQueryable<Message> GetIncomingMessages(User usr)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Message> GetOutcomingMessages(User usr)
        {
            throw new NotImplementedException();
        }

        public IQueryable<User> GetFollowers(User usr)
        {
            throw new NotImplementedException();
        }

        public IQueryable<User> GetCommonFollowers(User usrFirst, User usrSecond)
        {
            throw new NotImplementedException();
        }

        public User FindById(int id)
        {
            throw new NotImplementedException();
        }

        public User FindByUsername(string name)
        {

            // TODO vymyslet lepší způsob, protože v tomhle za chvíli bude zmatek

            User user1 = User.CreateUser(0, "Jindra", "Hrnčír", DateTime.Now,
                  DateTime.Now, "hrncir.jindra@nebelvir.br", "hpotter", "alohomora");
            
            User user2 = User.CreateUser(1, "Tomáš", "Raddle", DateTime.Now,
                DateTime.Now, "tomas.raddle@zmijozel.br", "voltmetr", "avadaKadevra");

            Message firstMsg = Message.CreateMessage(0, "Text", DateTime.Now);
            firstMsg.Sender = user1;
            user1.SentMessages.Add(firstMsg);
            firstMsg.Recievers.Add(user2);
            user2.Messages.Add(firstMsg);

            Message secondMsg = Message.CreateMessage(0, "Text", DateTime.Now);
            //secondMsg.
            secondMsg.Sender = user2;
            user2.SentMessages.Add(secondMsg);
            secondMsg.Recievers.Add(user1);
            user1.Messages.Add(secondMsg);
            
            return user1;
        }

        public bool IsUserInGroup(User usr, Group grp)
        {
            throw new NotImplementedException();
        }

        public bool IsEmailUnique(string email, User usr)
        {
            throw new NotImplementedException();
        }

        public void Add(User user)
        {
            throw new NotImplementedException();
        }

        public void Edit(User user)
        {
            throw new NotImplementedException();
        }

        public void Delete(User usr)
        {
            throw new NotImplementedException();
        }

        public void JoinGroup(User usr, Group grp)
        {
            throw new NotImplementedException();
        }

        public void LeaveGroup(User usr, Group grp)
        {
            throw new NotImplementedException();
        }

        public void FollowUser(User user, User follower)
        {
            throw new NotImplementedException();
        }

        public void AddStatus(User user, Status st)
        {
            throw new NotImplementedException();
        }
    }
}
