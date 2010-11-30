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
			try
			{
				var user = Model.UserService.FindById(id);
				Model.UserService.FollowUser(user, CurrentUser);

				var message = "You started following " + user.FullName + ".";

				if (Request.IsAjaxRequest())
				{
					return AjaxFlashMessage(message);
				}
				else
				{
					FlashMessage(message);
				}
			}
			catch (UserException)
			{
				var message = "You already follow user.";

				if (Request.IsAjaxRequest())
				{
					return AjaxFlashMessage(message, FLASH_ERROR);
				}
				else
				{
					FlashMessage(message, FLASH_ERROR);
				}
			}

			return RedirectToAction("Followings", new { username = CurrentUser.Username });
        }


		[Authorize]
		public ActionResult UnfollowUser(int id)
		{
			try
			{
				var user = Model.UserService.FindById(id);
				Model.UserService.UnfollowUser(user, CurrentUser);

				var message = "You stopped following " + user.FullName + ".";

				if (Request.IsAjaxRequest())
				{
					return AjaxFlashMessage(message);
				}
				else
				{
					FlashMessage(message);
				}
			}
			catch (UserException)
			{
				var message = "You are not following this user.";

				if (Request.IsAjaxRequest())
				{
					return AjaxFlashMessage(message, FLASH_ERROR);
				}
				else
				{
					FlashMessage(message, FLASH_ERROR);
				}
			}

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
