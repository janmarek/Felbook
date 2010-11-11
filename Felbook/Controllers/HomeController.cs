using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Felbook.Models;

namespace Felbook.Controllers
{
    [HandleError]
    public class HomeController : FelbookController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TestData()
        {
            Felbook.Models.TestData.Insert();
            return View();
        }
    }
}