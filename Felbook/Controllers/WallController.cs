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
    public class WallController : FelbookController
    {
		[Authorize]
        public ActionResult Index()
        {
			Model.WallService.MarkAllWallItemsRead(CurrentUser);

			return View(new WallViewModel { 
				CurrentUser = CurrentUser,
				WallItems = Model.WallService.GetWall(CurrentUser),
			});
        }

    }
}
