using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Felbook.Models;

namespace Felbook.Tests.Fakes
{
    class MockStatusService : IStatusService
    {
        public Status FindStatusById(int id)
        {
            throw new NotImplementedException();
        }

        public void AddCommentToStatus(User author, Status commentedStatus, string text)
        {
            throw new NotImplementedException();
        }

        public void AddStatus(User user, Group group, StatusFormModel formModel)
        {
            throw new NotImplementedException();
        }

        public void AddStatus(User user, StatusFormModel formModel)
        {
            throw new NotImplementedException();
        }
    }
}
