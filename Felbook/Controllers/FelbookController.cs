using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Felbook.Models;

namespace Felbook.Controllers
{
    public abstract class FelbookController : Controller
    {
		protected const string FLASH_SUCCESS = "success";

		protected const string FLASH_ERROR = "error";

		#region properties

		public IModel Model { get; set; }

		public User CurrentUser
		{
			get
			{
				if (Request.IsAuthenticated)
				{
					return Model.UserService.FindByUsername(User.Identity.Name);
				}
				else
				{
					return null;
				}
			}
		}

		#endregion

		#region construct
		
		public FelbookController(IModel model)
		{
			Model = model;
		}


		public FelbookController()
			: this(new Model())
		{

		}

		#endregion

		public void FlashMessage(string message, string type = FLASH_SUCCESS)
		{
			TempData["flashMessage"] = message;
			TempData["flashType"] = type;
		}

		public JsonResult AjaxFlashMessage(string message, string type = FLASH_SUCCESS)
		{
			return Json(new {
				flash = new {
					message = message,
					type = type,
				}
			}, JsonRequestBehavior.AllowGet);
		}

    }
}
