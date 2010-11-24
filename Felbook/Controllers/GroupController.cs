using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Felbook.Models;
using Felbook.Helpers;

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
    public class GroupController : FelbookController
	{
		#region actions

		#region find

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

		#endregion

		#region detail + join + leave
		
		/// <summary>
		/// Zobrazit skupinu
		/// </summary>
		/// <param name="id">id</param>
		/// <returns></returns>
		public ActionResult Detail(int id)
		{
			return View(new GroupViewModel {
				CurrentUser = CurrentUser,
				Group = Model.GroupService.FindById(id),
				ImageOutput = new ImageOutputHelper(Model.ImageService),
				FileOutput = new FileOutputHelper(Model.FileService),
			});
		}


		/// <summary>
		/// Přidat se ke skupině
		/// </summary>
		/// <param name="id">id</param>
		/// <returns></returns>
		public ActionResult Join(int id)
		{
			var group = Model.GroupService.FindById(id);
			Model.UserService.JoinGroup(CurrentUser, group);
			return RedirectToAction("Detail", new { id = id });
		}


		/// <summary>
		/// Odejít ze skupiny
		/// </summary>
		/// <param name="id">id</param>
		/// <returns></returns>
		public ActionResult Leave(int id)
		{
			var group = Model.GroupService.FindById(id);
			Model.UserService.LeaveGroup(CurrentUser, group);
			return RedirectToAction("Detail", new { id = id });
		}

		#endregion

		#region add status

		[HttpPost, ValidateAntiForgeryToken]
		public ActionResult AddStatus(int id, StatusFormModel formModel)
		{
			Model.StatusService.AddStatus(CurrentUser, Model.GroupService.FindById(id), formModel);
			return RedirectToAction("Detail", new { id = id });
		}
		#endregion

		#region create

		/// <summary>
		/// Vytvořit skupinu
		/// </summary>
		/// <returns></returns>
		public ActionResult Create()
		{
			return View();
		}


		/// <summary>
		/// Vytvořit skupinu, zpracování formuláře
		/// </summary>
		/// <param name="group">skupina s daty naplněnými z formuláře</param>
		/// <returns></returns>
		[HttpPost, ValidateAntiForgeryToken]
		public ActionResult Create(Group group)
		{
			if (ModelState.IsValid)
			{
				Model.GroupService.Add(CurrentUser, group);
				return RedirectToAction("Detail", new { id = group.Id });
			}

			return View();
		}

		#endregion

		#region create subgroup

		public ActionResult CreateSubGroup(int id)
		{
			return View();
		}

		[HttpPost]
		public ActionResult CreateSubGroup(int id, Group group)
		{
			var parent = Model.GroupService.FindById(id);
			
			if (ModelState.IsValid)
			{
				Model.GroupService.AddSubGroup(CurrentUser, parent, group);
				return RedirectToAction("Detail", new { id = group.Id });
			}
			
			return View();
		}

		#endregion

		#region edit group

		public ActionResult Edit(int id)
		{
			return View(Model.GroupService.FindById(id));
		}


		[HttpPost, ValidateAntiForgeryToken]
		public ActionResult Edit(int id, FormCollection collection)
		{
			var group = Model.GroupService.FindById(id);

			TryUpdateModel(group);

			if (ModelState.IsValid)
			{
				Model.GroupService.Edit(group);
				return RedirectToAction("Detail", new { id = group.Id });
			}

			return View(group);
		}

		#endregion

		#endregion

	}
}
