using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Felbook.Models;
using System.Web.Routing;

#region view models

namespace Felbook.Models
{
	public class FollowersViewModel
	{
		public User CurrentUser { get; set; }
		
		public User User { get; set; }

		public IEnumerable<User> Followers { get; set; }
	}

	public class FollowingsViewModel
	{
		public User CurrentUser { get; set; }

		public User User { get; set; }

		public IEnumerable<User> Followings { get; set; }
	}
}

#endregion

namespace Felbook.Controllers
{
    public class UserController : FelbookController
	{
		[Authorize]
		public ActionResult FollowUser(int id)
        {
			var user = Model.UserService.FindById(id);
			Model.UserService.FollowUser(user, CurrentUser);

			return RedirectToAction("Followings", new { username = CurrentUser.Username });
        }


		public ActionResult Followers(string username)
		{
			var user = Model.UserService.FindByUsername(username);

			return View(new FollowersViewModel {
				CurrentUser = CurrentUser,
				User = user,
				Followers = user.Followers
			});
		}


		public ActionResult Followings(string username)
		{
			var user = Model.UserService.FindByUsername(username);

			return View(new FollowingsViewModel {
				CurrentUser = CurrentUser,
				User = user,
				Followings = user.Followings,
			});
		}

    }
}
