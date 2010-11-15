using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.IO;
using Felbook.Models;
using Felbook.Helpers;

namespace Felbook.Controllers
{

	[HandleError]
	public class AccountController : FelbookController
	{

		public IFormsAuthenticationService FormsService { get; set; }
		public IFelbookMembershipService MembershipService { get; set; }

		protected override void Initialize(RequestContext requestContext)
		{
			if (FormsService == null) { FormsService = new FormsAuthenticationService(); }
			if (MembershipService == null) { 
                
                //MembershipService = new AccountMembershipService(); 
                FelBookDBEntities db = new FelBookDBEntities();
                MembershipService = new FelbookAccountMembershipService(db);
            
            }

			base.Initialize(requestContext);
		}

		// **************************************
		// URL: /Account/LogOn
		// **************************************

		public ActionResult LogOn()
		{
			return View();
		}

		[HttpPost]
		public ActionResult LogOn(LogOnModel model, string returnUrl)
		{
			if (ModelState.IsValid) {
				if (MembershipService.ValidateUser(model.UserName, model.Password)) {
					FormsService.SignIn(model.UserName, model.RememberMe);
					if (!String.IsNullOrEmpty(returnUrl)) {
						return Redirect(returnUrl);
					} else {
						return RedirectToAction("Index", "Home");
					}
				} else {
					ModelState.AddModelError("", "The user name or password provided is incorrect.");
				}
			}

			// If we got this far, something failed, redisplay form
			return View(model);
		}

		// **************************************
		// URL: /Account/LogOff
		// **************************************

		public ActionResult LogOff()
		{
			FormsService.SignOut();

			return RedirectToAction("Index", "Home");
		}

		// **************************************
		// URL: /Account/Register
		// **************************************

		public ActionResult Register()
		{
			ViewData["PasswordLength"] = MembershipService.MinPasswordLength;
			return View();
		}

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            Felbook.Helpers.Image imageOperator = new Felbook.Helpers.Image();
            HttpPostedFileBase imageToUpload = Request.Files["profileimage"];
            bool uploadImage = false;

            if (imageToUpload.ContentLength == 0)
            {
                uploadImage = false;
            }
            else if (imageOperator.IsImage(imageToUpload))
            {
                uploadImage = true;
            }
            else {
                ModelState.AddModelError("file", "Your file wasn't image.");
            }   
                  
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                //MembershipCreateStatus createStatus = MembershipService.CreateUser(model.UserName, model.Password, model.Email);
                MembershipCreateStatus createStatus = MembershipService.CreateUser(model);
                if (createStatus == MembershipCreateStatus.Success)
                {
                    FormsService.SignIn(model.UserName, false /* createPersistentCookie */);

                    //upload user profile image 
                    User actualUser = Model.UserService.FindByUsername(model.UserName);
                    int userId = actualUser.Id;
                    string fileDir = "../Web_Data/profile_images/";
                    //string fileExtension = Path.GetExtension(imageToUpload.FileName).Substring(1).ToLower();
                    string fileFullPath = Path.Combine(HttpContext.Server.MapPath(fileDir + userId), "profileimage" + ".png" /*+ fileExtension*/);
                    string fileDirPath = Path.GetDirectoryName(fileFullPath);

                    try
                    {
                        //pokusíme se vytvořit adresář
                        Directory.CreateDirectory(fileDirPath);
                    }
                    //jednotlivě odchytávám chyby
                    catch (UnauthorizedAccessException)
                    {
                        ModelState.AddModelError("file", "You have not permission to save file");
                    }
                    catch (Exception)
                    {
                        ModelState.AddModelError("file", "Some uknown error");
                    }

                    if (uploadImage == true)
                    {
                        imageOperator.ImageResize(imageToUpload, fileFullPath, 90, 120);
                    }
                    else 
                    { 
                        //zkopiruje defaultni obrazek
                        string fileName = "profileimage.png";

                        // Use Path class to manipulate file and directory paths.
                        string sourceFile = Path.Combine(HttpContext.Server.MapPath(fileDir + "/default/"), fileName);
                        string destFile = System.IO.Path.Combine(fileDirPath, fileName);

                        // To copy a file to another location and 
                        // overwrite the destination file if it already exists.
                        System.IO.File.Copy(sourceFile, destFile, true);
                    }
                    //return View("Index", actualUser);
                    return RedirectToAction("Index", "Profile", new { username = actualUser.Username });
                    
                    ///Profile?username=novakjan
                    //return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", AccountValidation.ErrorCodeToString(createStatus));
                }
                

                    
            }

            // If we got this far, something failed, redisplay form
            ViewData["PasswordLength"] = MembershipService.MinPasswordLength;
            return View(model);
        }

		// **************************************
		// URL: /Account/ChangePassword
		// **************************************

		[Authorize]
		public ActionResult ChangePassword()
		{
			ViewData["PasswordLength"] = MembershipService.MinPasswordLength;
			return View();
		}

		[Authorize]
		[HttpPost]
		public ActionResult ChangePassword(ChangePasswordModel model)
		{
			if (ModelState.IsValid) {
				if (MembershipService.ChangePassword(User.Identity.Name, model.OldPassword, model.NewPassword)) {
					return RedirectToAction("ChangePasswordSuccess");
				} else {
					ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
				}
			}

			// If we got this far, something failed, redisplay form
			ViewData["PasswordLength"] = MembershipService.MinPasswordLength;
			return View(model);
		}

		// **************************************
		// URL: /Account/ChangePasswordSuccess
		// **************************************

		public ActionResult ChangePasswordSuccess()
		{
			return View();
		}

	}
}
