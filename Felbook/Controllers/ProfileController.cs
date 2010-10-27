using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Felbook.Models;
using System.Web.Routing;
using System.IO;
using System.Security.AccessControl;
using System.Management;
using System.Management.Instrumentation;

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

        public bool ThumbnailCallback()
        {
            return false;
        }


        [AcceptVerbs(HttpVerbs.Post), HttpPost]
        public ActionResult AddStatus(FormCollection collection)
        {
            UserService userSer = new UserService(); //používám z modelu UserService
            User actualUser = userSer.SearchByUserName(User.Identity.Name);
            int userId = actualUser.Id; //vytáhnu si ID usera pro vytvoření složky
            Status status = new Status();
            status.Text = collection["status"];
            status.Created = DateTime.Now;


            HttpPostedFileBase file = Request.Files["picture"];
            //ověření jestli je to obrázek
            if (file.ContentType == "image/jpeg" && (file.FileName.ToLower().EndsWith(".jpg") || file.FileName.ToLower().EndsWith(".jpeg"))
                || file.ContentType == "image/gif" && file.FileName.ToLower().EndsWith(".gif")
                || file.ContentType == "image/png" && file.FileName.ToLower().EndsWith(".png")
                )
            {
                if (file.ContentLength > 0)
                {
                    string pathDir = "../Web_Data/status_images/";
                    string filePath = Path.Combine(HttpContext.Server.MapPath(pathDir + userId), Path.GetFileName(file.FileName));
                    string dirPath = Path.GetDirectoryName(filePath);

                    if (System.IO.File.Exists(filePath) == true)
                    {
                        ModelState.AddModelError("picture", "This file " + file.FileName + " is already exist.");
                    }

                    if (Directory.Exists(dirPath) == false)
                    {
                        try
                        {
                            //pokusíme se vytvořit adresář
                            Directory.CreateDirectory(dirPath);
                        }
                        //jednotlivě odchytávám chyby
                        catch (UnauthorizedAccessException)
                        {
                            ModelState.AddModelError("picture", "You have not permission to save picture");
                        }
                        catch (Exception)
                        {
                            ModelState.AddModelError("picture", "Some uknown error");
                        }
                    }

                    //ukládám soubor zase s odchytavanim chyb
                    try
                    {
                        file.SaveAs(filePath);
                        Felbook.Models.Image newImg = new Felbook.Models.Image();
                        if (String.IsNullOrEmpty(collection["description"]))
                        {
                            newImg.Description = "no comment"; //pokud uživatel nevyplnil description
                        }
                        else
                        {
                            newImg.Description = collection["description"];
                        }
                        newImg.FileName = file.FileName;
                        status.Images.Add(newImg);
                    }
                    catch (UnauthorizedAccessException)
                    {
                        ModelState.AddModelError("picture", "You have not permission to save picture");
                    }
                    catch (DirectoryNotFoundException)
                    {
                        ModelState.AddModelError("picture", "You have not permission to save picture");
                    }
                    catch (Exception)
                    {
                        ModelState.AddModelError("picture", "Some uknown error");
                    }
                }
            }
            else
            {
                ModelState.AddModelError("picture", "This file " + file.FileName + " is not image.");
            }




            userSer.AddStatus(actualUser, status); //uloží se status i s obrázkem

            if (!ModelState.IsValid)
            {
                return View("Index", actualUser);
            }
            else
            {
                userSer.Save();
                return RedirectToAction("Index", new { username = User.Identity.Name });
            }
        }

    }
}
