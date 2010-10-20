using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Felbook.Models;

namespace Felbook.Controllers
{
	[HandleError]
	public class HomeController : Controller
	{
		public ActionResult Index()
		{

            FelBookDBEntities db = new FelBookDBEntities(
                    "metadata=\"C:\\Users\\Administrator\\Documents\\Felbook\\Felbook\\obj\\Debug\\edmxResourcesToEmbed\\Models\";provider=System.Data.SqlClient;provider connection string=\"Data Source=VIRTUAL-WIN2008\\SQLEXPRESS;Initial Catalog=FelBookDB;Integrated Security=True\"");
            
            if (User.Identity.Name != "")
            {
                User user = db.UserSet.Single(u => u.Username == User.Identity.Name);
                ViewData["Message"] = "Jsem příhlášený jako: " + user.Username; 
                ViewData["Message2"] = "Moje jméno je: " + user.Name + " " + user.Surname;
                ViewData["Message3"] = "Jsem přihlášený od: " + user.LastLogged;
            }
            else
            {
                ViewData["Message"] = "Nejsem přihlášený!";
            }
            
            return View();
		}

		public ActionResult About()
		{
			return View();
		}
	}
}
