using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Felbook.Models;

namespace Felbook.Models
{
    public class CommentModel
    {
        public string Text { get; set; }

        public string PrevUrl { get; set; }

        public int StatusID { get; set; }
    }
}

namespace Felbook.Controllers
{
    public class StatusController : FelbookController
    {
        
        [AcceptVerbs(HttpVerbs.Post), HttpPost]
        public ActionResult AddComment(CommentModel model)
        {
            if (!String.IsNullOrWhiteSpace(model.Text))
            {
                Model.StatusService.AddCommentToStatus(CurrentUser, Model.StatusService.FindStatusById(model.StatusID), model.Text);
            }
            return Redirect(model.PrevUrl);
        }
    }
}
