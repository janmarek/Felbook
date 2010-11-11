using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Felbook.Models;

namespace Felbook.Controllers
{
    public abstract class FelbookController : Controller
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
		
		public FelbookController(Model model)
		{
			Model = model;
		}


		public FelbookController()
			: this(new Model())
		{

		}

		#endregion

    }
}
