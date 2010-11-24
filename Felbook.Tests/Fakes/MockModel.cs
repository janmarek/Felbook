using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Felbook.Models;

namespace Felbook.Tests.Fakes
{
    class MockModel : IModel
    {
        public IGroupService GroupService 
        {
            get
            {
                return new MockGroupService();
            }
        }
        
        public IUserService UserService
        {
            get
            {
                return new MockUserService();
            }
        }
        
        public IMessageService MessageService
        {
            get
            {
                return new MockMessageService();
            }
        }

        public IWallService WallService
        {
            get
            {
                return new MockWallService();
            }
        }

        public IStatusService StatusService
        {
            get
            {
                return new MockStatusService();
            }
        }
    }
}
