using System.Web.Mvc;
using Felbook.Models;
using Felbook.Helpers;
using System.Web;
using System.IO;
using System;

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



		[Authorize]
		public ActionResult Edit()
		{
			return View(CurrentUser);
		}


        [Authorize]
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit(Felbook.Models.User model) //FormCollection collection
        {
            TryUpdateModel(CurrentUser);

            //upload změna profilového obrázku
            Felbook.Helpers.ImageHelper imageOperator = new Felbook.Helpers.ImageHelper(); //pomocná třída pro operace s obrázky
            HttpPostedFileBase imageToUpload = Request.Files["profileimage"];
            int userId = CurrentUser.Id;
            string fileDir = "../Web_Data/profile_images/";
            //název souboru je vždy stejný
            string fileName = "profileimage.png";
            string fileFullPath = Path.Combine(HttpContext.Server.MapPath(fileDir + userId), fileName);
            string fileDirPath = Path.GetDirectoryName(fileFullPath);
            bool uploadImage = false;

            if (imageToUpload.ContentLength == 0)
            {
                uploadImage = false;
            }
            else if (Felbook.Helpers.ImageHelper.IsImage(imageToUpload.ContentType))
            {
                uploadImage = true;
            }
            else
            {
                ModelState.AddModelError("file", "Your file wasn't image.");
            }

            if (model.OldPassword != null && String.Equals(model.Password, model.ConfirmPassword))
            {
                if (CurrentUser.CheckPassword(model.OldPassword))
                {
                    CurrentUser.ChangePassword(model.Password);
                }
                else
                {
                    ModelState.AddModelError("", "The password provided is incorrect.");
                }
            }

            if (ModelState.IsValid)
            {
                //zpráva pro uživatele že editoval profil bez problému
                ViewData["EditResult"] = "The edit is successful.";
                
                if (uploadImage == true)
                {
                    try
                    {
                        System.IO.File.Delete(fileFullPath);
                    }
                    catch (Exception)
                    {
                        ModelState.AddModelError("file", "Unexpected file error.");
                        return View(CurrentUser);
                    }
                    imageOperator.ImageResize(imageToUpload, fileFullPath, 90, 120);
                }

                Model.UserService.Edit(CurrentUser);
                return View(CurrentUser);
            }
            //v případě nějaké chyby se vrátí tohle
            return View(CurrentUser);
        }

		[HttpPost, ValidateAntiForgeryToken, Authorize]
		public ActionResult AddStatus(StatusFormModel formModel)
		{
			Model.StatusService.AddStatus(CurrentUser, formModel);
			return RedirectToAction("Index", new { username = CurrentUser.Username });
		}

    }
}
