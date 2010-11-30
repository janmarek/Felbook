using System.Web.Mvc;
using Felbook.Models;
using Felbook.Helpers;

#region view models
namespace Felbook.Models
{
	public class ProfileViewModel
	{
		#region variables
		private User user;
		private User currentUser;
		private ImageOutputHelper imageOutputHelper;
		private FileOutputHelper fileOutputHelper;
		#endregion

		#region properties
		public User User { get { return user; } }
		public User CurrentUser { get { return currentUser; } }
		#endregion

		public ProfileViewModel(User user, User currentUser, IImageService imageService, IFileService fileService)
		{
			this.user = user;
			this.currentUser = currentUser;
			this.imageOutputHelper = new ImageOutputHelper(imageService);
			this.fileOutputHelper = new FileOutputHelper(fileService);
		}

		public StatusViewModel CreateStatusViewModel(Status status)
		{
			return new StatusViewModel { Status = status, FileOutput = fileOutputHelper, ImageOutput = imageOutputHelper };
		}
	}
}
#endregion

namespace Felbook.Controllers
{
    public class ProfileController : FelbookController
    {

        public ActionResult Index(string username)
        {
            User user = Model.UserService.FindByUsername(username);
            return View(new ProfileViewModel(user, CurrentUser, Model.ImageService, Model.FileService));
        }



		public ActionResult Edit()
		{
			return View(CurrentUser);
		}



		[HttpPost, ValidateAntiForgeryToken]
		public ActionResult Edit(FormCollection collection)
		{
			TryUpdateModel(CurrentUser);

			if (ModelState.IsValid)
			{
				Model.UserService.Edit(CurrentUser);
				return RedirectToAction("Index", new { username = CurrentUser.Username });
			}

			return View(CurrentUser);
		}



		[HttpPost, ValidateAntiForgeryToken]
		public ActionResult AddStatus(StatusFormModel formModel)
		{
			Model.StatusService.AddStatus(CurrentUser, formModel);
			return RedirectToAction("Index", new { username = CurrentUser.Username });
		}

    }
}
