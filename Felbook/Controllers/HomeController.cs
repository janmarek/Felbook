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
			Felbook.Models.FelbookDataContext db = new Models.FelbookDataContext();

            //ViewData["Message"] = db.Informations.Single(i => i.InfoAboutUser == 10 ).getContent();

            return View();
		}

		public ActionResult About()
		{
			return View();
		}
	}
}
