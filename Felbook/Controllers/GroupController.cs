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
		/// Find group action
		/// </summary>
		/// <param name="search">search string</param>
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


		public ActionResult Detail(int id)
		{
			var group = Model.GroupService.FindById(id);
			return View(group);
		}

		

    }
}
