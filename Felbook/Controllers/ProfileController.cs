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
using System.Drawing.Imaging;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text.RegularExpressions;



namespace Felbook.Controllers
{
    public class ProfileController : Controller
    {
        #region Proměnné
        /// <summary>
        /// Znak podle kterého se budou dělit strinq linků do pole
        /// </summary>
        #endregion

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

        protected override void Initialize(RequestContext requestContext)
        {
            if (Model == null)
            {
                Model = new Model();
            }

            base.Initialize(requestContext);
        } 

        public ActionResult Index(string username)
        {
            User user = Model.UserService.FindByUsername(username);
            return View(user);
        }

		public ActionResult Edit()
		{
			return View(CurrentUser);
		}

		[HttpPost, ValidateAntiForgeryToken]
		public ActionResult Edit(FormCollection collection)
		{
			TryUpdateModel(CurrentUser);

			if (ModelState.IsValid)
			{
				Model.UserService.Edit(CurrentUser);
				return RedirectToAction("Index", new { username = CurrentUser.Username });
			}

			return View(CurrentUser);
		}

        public bool ThumbnailCallback()
        {
            return false;
        }

        

        // AJAX: /Profile/GetContent/
        /// <summary>
        /// Tato metoda řeší parsování a kontrolování zadaných linků od uživatele
        /// </summary>
        /// <param name="collection">Kolekce hodnot z formuláře</param>
        /// <returns>Vrátí obsah který se uloží 
        /// do DIV elementu ve formuláři, a po odeslání formuláře se z SESSION 
        /// vrátí JSON hodnotu do javascriptu clienta</returns>
        [HttpPost]
        public ActionResult SetLinksContent(FormCollection collection)  
        {
            string link = collection["newLink"];
            link = link.TrimEnd();
            link = link.TrimStart();
            if (link.Trim().Equals(String.Empty)) {
                return Json("null");
            }

            if (link.StartsWith("http://") == false && link.StartsWith("https://") == false && link.StartsWith("ftp://") == false) 
            {
                link = "http://" + link;
            }
            
            string pattern = @"^(http|https|ftp)\://+[a-zA-Z]{2,3}(:[a-zA-Z0-9]*)?/?([a-zA-Z0-9\-\._\?\,\'/\\\+&amp;%\$#\=~])*[^\.\,\)\(\s]$";
            Regex reg = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            if (reg.IsMatch(link) && link.Split('.').Length > 1 && link.Length > 4 && link.Contains(',') == false)
            {

                return Json(link);            
            }
            else {
                return Json("error");
            }
        }

        /// <summary>
        /// Metoda která změní velikost obrázku a rovnou ho uploaduje
        /// </summary>
        /// <param name="file"></param>
        /// <param name="filePath"></param>
        private void ImageResize(HttpPostedFileBase file, string filePath)
        {
         if (file != null && file.FileName != "")
         {
             int maxWidth = 800; //maximální šířka
             int maxHeight = 600; //maximální výška

            string strExtension = System.IO.Path.GetExtension(file.FileName);
            if ((strExtension.ToUpper() == ".JPG") | (strExtension.ToUpper() == ".GIF") | (strExtension.ToUpper() == ".PNG"))
            {
             // změní velikost obrázku
              System.Drawing.Image imageToBeResized = System.Drawing.Image.FromStream(file.InputStream);
              int imageHeight = imageToBeResized.Height;
              int imageWidth = imageToBeResized.Width;
              imageHeight = (imageHeight * maxWidth) / imageWidth;
              imageWidth = maxWidth;

                      if (imageHeight > maxHeight)
                        {
                            imageWidth = (imageWidth * maxHeight) / imageHeight;
                            imageHeight = maxHeight;
                        }

                        Bitmap bitmap = new Bitmap(imageToBeResized, imageWidth, imageHeight);
                        bitmap.Save(filePath);        
                    }
                    }
        }

        [AcceptVerbs(HttpVerbs.Post), HttpPost]
        public ActionResult AddStatus(FormCollection collection)
        {
            User actualUser = Model.UserService.FindByUsername(User.Identity.Name);
            int userId = actualUser.Id; //vytáhnu si ID usera pro vytvoření složky
            Status status = new Status();
            status.Text = collection["status"];
            status.Created = DateTime.Now;
            
            string [] links = null;
            if (collection["link"] != null) {
                links = collection["link"].Split(',');
            }         

            //representace souborů k uploadu
            List<HttpPostedFileBase> filesToSave = new List<HttpPostedFileBase>();
            //cesty kam se budou soubory ukládat
            List<string> filesPathsToSave = new List<string>();

            int pictureOrder = 1; //pictureOrder je proměnná určující pořadí obrázku
            //int fileOrder = 1; //fileOrder je proměnná určující pořadí obrázku
            while (Request.Files.Count >= pictureOrder /*&& Request.Files["picture" + fileOrder].ContentLength > 0*/)
            {
                HttpPostedFileBase file = null;
                if (/*Request.Files["picture" + pictureOrder] != null && */Request.Files["picture" + pictureOrder].ContentLength > 0)
                {
                    file = Request.Files["picture" + pictureOrder];               
                    
                    //ověření jestli je to obrázek
                    if (file.ContentType == "image/jpeg" && (file.FileName.ToLower().EndsWith(".jpg") || file.FileName.ToLower().EndsWith(".jpeg"))
                        || file.ContentType == "image/gif" && file.FileName.ToLower().EndsWith(".gif")
                        || file.ContentType == "image/png" && file.FileName.ToLower().EndsWith(".png")
                        )
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
                            //ke statusu se postupně ukládají informace ohledně uploadu
                            //jestli se opravdu uploadujou obrázky se rozhodne na konci metody
                            Felbook.Models.Image newImg = new Felbook.Models.Image();
                            if (String.IsNullOrEmpty(collection["description" + pictureOrder]))
                            {
                                newImg.Description = "no comment"; //pokud uživatel nevyplnil description
                            }
                            else
                            {
                                newImg.Description = collection["description" + pictureOrder];
                            }
                            newImg.FileName = file.FileName;
                            status.Images.Add(newImg);
                            filesToSave.Add(file);
                            filesPathsToSave.Add(filePath);
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
                    else
                    {
                        ModelState.AddModelError("picture", "This file " + file.FileName + " is not image.");
                    }
                }
                pictureOrder++; //inkrementace pro načtení dalšího uploadvatelného obrázku
            }
              
            if (!ModelState.IsValid) //pokud model není validní tak se nic neuloží a pouze se ukážou chyby pomocí View[]
            {
                return View("Index", actualUser);
            }
            else //pokud je model validní tak uloží user linky, uploaduje a uloží obrázky a uloží samotný status k uživately
            {
                //upload linků                    
                if (links != null) { 
                foreach(string link in links)
                    {
                        Felbook.Models.Link newLink = new Felbook.Models.Link();
                        newLink.URL = link;
                        status.Links.Add(newLink);
                    }
                }
                //nyní upload obrázku ke statusu
                int filePointer = 0; //ukazatel abych ukazoval vzdy na spravnou cestu k souboru
                foreach (HttpPostedFileBase file in filesToSave)
                { //projdou ve vsechny soubory a uploadujou se
                    this.ImageResize(file, filesPathsToSave.ElementAt(filePointer));
                    filePointer++;
                }
                Model.UserService.AddStatus(actualUser, status); //uloží se status i s obrázkem
                return RedirectToAction("Index", new { username = User.Identity.Name });
            }
        }

    }
}
