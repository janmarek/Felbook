using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Felbook.Models;

namespace Felbook.Tests.Fakes
{
    class MockWallService : AbstractMockService, IWallService
    {

        public MockWallService(MockModel model) : base(model) { }
        
        public void Add(Status status, IEnumerable<User> users)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<WallItem> GetWall(User user, int limit = 20)
        {
            throw new NotImplementedException();
        }

        public int GetUnreadCount(User user)
        {
            throw new NotImplementedException();
        }

        public void MarkAllWallItemsRead(User user)
        {
            throw new NotImplementedException();
        }
    }
}
