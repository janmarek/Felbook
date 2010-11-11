using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Felbook.Models;

namespace Felbook.Models
{
	public class WallViewModel
	{
		public IEnumerable<WallItem> WallItems { get; set; }

		public User CurrentUser { get; set; }
	}
}

namespace Felbook.Controllers
{
    public class WallController : Controller
    {	
		#region properties

		public Model Model { get; set; }

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
		
		public WallController(Model model)
		{
			Model = model;
		}


		public WallController()
			: this(new Model())
		{

		}

		#endregion

        public ActionResult Index()
        {
			return View(new WallViewModel { 
				CurrentUser = CurrentUser,
				WallItems = Model.WallService.GetWall(CurrentUser),
			});
        }

    }
}
