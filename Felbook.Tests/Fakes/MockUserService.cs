using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Felbook.Models;

namespace Felbook.Tests.Fakes
{
    class MockUserService : AbstractMockService, IUserService
    {

        public MockUserService(MockModel model) : base(model) { }

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
            return model.UserList.Single(m => m.Username == name);
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

        public void UnfollowUser(User user, User follower)
        {
            throw new NotImplementedException();
        }
    }
}
