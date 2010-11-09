using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Felbook.Models
{
	/// <summary>
	/// View model pro follow link partial view
	/// </summary>
	public class FollowLinkViewModel
	{
		public User CurrentUser { get; set; }

		public User Follower { get; set; }


		public FollowLinkViewModel()
		{

		}


		public FollowLinkViewModel(User currentUser, User follower)
		{
			CurrentUser = currentUser;
			Follower = follower;
		}		
	
	}


	/// <summary>
	/// View model pro výpis uživatelů
	/// </summary>
	public class UserListViewModel
	{
		
		public User CurrentUser { get; set; }

		public IEnumerable<User> Users { get; set; }


		public UserListViewModel()
		{

		}


		public UserListViewModel(User currentUser, IEnumerable<User> users)
		{
			CurrentUser = currentUser;
			Users = users;
		}	
	}
}