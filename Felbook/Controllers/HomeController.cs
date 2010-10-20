using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Felbook.Controllers
{
	[HandleError]
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
            Felbook.Models.FelBookDBEntities db = new Models.FelBookDBEntities();
            ViewData["Message"] = db.UserSet.Single(i => i.Id == 1).Name.ToString();          
            return View();
		}

		public ActionResult About()
		{
			return View();
		}
	}
}
