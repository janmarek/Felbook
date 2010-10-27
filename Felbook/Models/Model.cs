using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Felbook.Models
{
	public class Model
	{
		#region Variables

		private IGroupService groupService = null;

		private IUserService userService = null;

		#endregion

		#region Properties

		protected FelBookDBEntities DBEntities { get; set; }

		public IGroupService GroupService
		{
			get
			{
				if (groupService == null)
				{
					groupService = new GroupService(DBEntities);
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
					userService = new UserService(DBEntities);
				}

				return userService;
			}
		}

		#endregion

		public Model()
		{
			DBEntities = new FelBookDBEntities();
		}
	}
}