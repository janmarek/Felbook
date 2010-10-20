using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Felbook.Models;
using System.Web.Routing;

namespace Felbook.Controllers
{
    public class ProfileController : Controller
    {
		public FelBookDBEntities DbEntities { get; set; }

		protected override void Initialize(RequestContext requestContext)
		{
			if (DbEntities == null)
			{
				DbEntities = new FelBookDBEntities();
			}

			base.Initialize(requestContext);
		}

        public ActionResult Index(string username)
        {
			User user = DbEntities.UserSet.Single(u => u.Username == username);
            return View(user);
        }


		[AcceptVerbs(HttpVerbs.Post), HttpPost]
		public ActionResult AddStatus(FormCollection collection)
		{
			Status status = new Status();
			status.Text = collection["status"];
			status.User = DbEntities.UserSet.Single(u => u.Username == User.Identity.Name);
			status.Created = DateTime.Now;
			DbEntities.AddToStatusSet(status);
			DbEntities.SaveChanges();
			return RedirectToAction("Index", new {username = User.Identity.Name});
		}

    }
}
