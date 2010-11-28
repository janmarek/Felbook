using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Felbook.Models;

namespace Felbook.Tests.Fakes
{
    class MockGroupService : AbstractMockService, IGroupService
    {

        public MockGroupService(MockModel model) : base(model) { }
        
        public void Add(User user, Group grp)
        {
            throw new NotImplementedException();
        }

        public void AddSubGroup(User user, Group group, Group child)
        {
            throw new NotImplementedException();
        }

        public void Delete(Group grp)
        {
            throw new NotImplementedException();
        }

        public IQueryable<User> GetUsers(Group grp)
        {
            throw new NotImplementedException();
        }

        public Group FindById(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Group> SearchGroups(string str)
        {
            throw new NotImplementedException();
        }

        public void Edit(Group group)
        {
            throw new NotImplementedException();
        }

        public Group FindByName(string name)
        {
            throw new NotImplementedException();
        }
    }
}
