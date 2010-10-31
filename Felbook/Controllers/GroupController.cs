using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Felbook.Models;

# region view models

namespace Felbook.Models
{
	public class FindGroupViewModel
	{
		public string SearchText { get; set; }

		public IQueryable<Group> Groups { get; set; }
	}

	public class GroupViewModel
	{
		public User CurrentUser { get; set; }

		public Group Group { get; set; }
	}
}

#endregion

namespace Felbook.Controllers
{
    public class GroupController : Controller
	{
		#region properties
		public Model Model { get; set; }
		#endregion

		#region init
		
		/// <summary>
		/// Initialize
		/// </summary>
		/// <param name="requestContext"></param>
		protected override void Initialize(RequestContext requestContext)
		{
			if (Model == null)
			{
				Model = new Model();
			}

			base.Initialize(requestContext);
		}

		#endregion


		/// <summary>
		/// Najít skupinu
		/// </summary>
		/// <param name="search">hledaný text</param>
		/// <returns>view</returns>
        public ActionResult Find(string search)
        {
			var viewModel = new FindGroupViewModel();

			if (search != null)
			{
				viewModel.SearchText = search;
				viewModel.Groups = Model.GroupService.SearchGroups(search);
			}

            return View(viewModel);
        }


		/// <summary>
		/// Zobrazit skupinu
		/// </summary>
		/// <param name="id">id</param>
		/// <returns></returns>
		public ActionResult Detail(int id)
		{
			return View(new GroupViewModel {
				CurrentUser = Model.UserService.FindByUsername(User.Identity.Name),
				Group = Model.GroupService.FindById(id),
			});
		}


		/// <summary>
		/// Přidat se ke skupině
		/// </summary>
		/// <param name="id">id</param>
		/// <returns></returns>
		public ActionResult Join(int id)
		{
			var currentUser = Model.UserService.FindByUsername(User.Identity.Name);
			var group = Model.GroupService.FindById(id);
			Model.UserService.JoinGroup(currentUser, group);
			return RedirectToAction("Detail", new { id = id });
		}


		/// <summary>
		/// Odejít ze skupiny
		/// </summary>
		/// <param name="id">id</param>
		/// <returns></returns>
		public ActionResult Leave(int id)
		{
			var currentUser = Model.UserService.FindByUsername(User.Identity.Name);
			var group = Model.GroupService.FindById(id);
			Model.UserService.LeaveGroup(currentUser, group);
			return RedirectToAction("Detail", new { id = id });
		}

		

    }
}
