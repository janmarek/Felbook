using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Felbook.Models
{
	public class Model
	{
		#region Variables

		private IGroupService groupService;

		private IUserService userService;

        private IMessageService messageService;

		private IWallService wallService;

		#endregion

		#region Properties

		protected FelBookDBEntities DBEntities { get; set; }

		public IGroupService GroupService
		{
			get
			{
				if (groupService == null)
				{
					groupService = new GroupService(DBEntities, WallService);
				}

				return groupService;
			}
		}

		public IUserService UserService
		{
			get
			{
				if (userService == null)
				{
					userService = new UserService(DBEntities, WallService);
				}

				return userService;
			}
		}

        public IMessageService MessageService
        {
            get
            {
                if (messageService == null)
                {
                    messageService = new MessageService(DBEntities);
                }

                return messageService;
            }
        }

		public IWallService WallService
		{
			get
			{
				if (wallService == null)
				{
					wallService = new WallService(DBEntities);
				}

				return wallService;
			}
		}

		#endregion

		public Model()
		{
			DBEntities = new FelBookDBEntities();
		}
	}
}