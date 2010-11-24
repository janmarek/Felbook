using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Felbook.Models;
using Felbook.Helpers;

#region view models
namespace Felbook.Models
{
	public class WallViewModel
	{
		public IEnumerable<WallItem> WallItems { get; set; }
		public User CurrentUser { get; set; }
		public ImageOutputHelper ImageOutput { get; set; }
		public FileOutputHelper FileOutput { get; set; }

		public StatusViewModel CreateStatusViewModel(Status status)
		{
			return new StatusViewModel { Status = status, FileOutput = FileOutput, ImageOutput = ImageOutput };
		}
	}
}
#endregion

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
				ImageOutput = new ImageOutputHelper(Model.ImageService),
				FileOutput = new FileOutputHelper(Model.FileService),
			});
        }

    }
}
