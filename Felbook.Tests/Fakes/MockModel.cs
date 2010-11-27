using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Felbook.Models;

namespace Felbook.Tests.Fakes
{
    class MockModel : IModel
    {

        // Tyto proměné nahrazují datové uložiště
        // Lze je v případě potřeby bez problémů přídávat
        public List<User> UserList;
        public List<Message> MessageList;

        public MockModel()
        {

            UserList = new List<User>();
            MessageList = new List<Message>();

        }

        #region Interface methods

        public IGroupService GroupService 
        {
            get
            {
                return new MockGroupService(this);
            }
        }
        
        public IUserService UserService
        {
            get
            {
                return new MockUserService(this);
            }
        }
        
        public IMessageService MessageService
        {
            get
            {
                return new MockMessageService(this);
            }
        }

        public IWallService WallService
        {
            get
            {
                return new MockWallService(this);
            }
        }

        public IStatusService StatusService
        {
            get
            {
                return new MockStatusService(this);
            }
        }
        
        public IFileService FileService
        {
            get 
            {
                return new MockFileService(this);
            }
        }

        public IImageService ImageService
        {
            get 
            {
                return new MockImageService(this);
            }
        }

        #endregion
    }

    abstract class AbstractMockService
    {
        protected MockModel model;

        public AbstractMockService(MockModel model)
        {
            this.model = model;
        }
    }
}
