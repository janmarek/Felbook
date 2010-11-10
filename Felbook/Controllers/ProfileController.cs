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

        

        /// <summary>
        /// Tato metoda řeší parsování a kontrolování zadaných linků od uživatele
        /// </summary>
        /// <param name="collection">Kolekce hodnot z formuláře - v tomto případě jeden textbox</param>
        /// <returns>Vrátí obsah který se uloží 
        /// do DIV elementu ve formuláři, a po odeslání formuláře
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

            //representace obrázků k uploadu
            List<HttpPostedFileBase> imagesToSave = new List<HttpPostedFileBase>();
            //cesty kam se budou obrázky ukládat
            List<string> imagesPathsToSave = new List<string>();

            //representace souborů k uploadu
            List<HttpPostedFileBase> filesToSave = new List<HttpPostedFileBase>();
            //cesty kam se budou soubory ukládat
            List<string> filesPathsToSave = new List<string>();

            int pictureOrder = 1; //pictureOrder je proměnná určující pořadí obrázku
            int fileOrder = 1; //fileOrder je proměnná určující pořadí obrázku
            
            
            //while (Request.Files.Count >= pictureOrder /*&& Request.Files["picture" + fileOrder].ContentLength > 0*/)
            //{

                HttpPostedFileBase fileToUpload = null;

                bool areFilesUploaded = false;
                while (areFilesUploaded==false) //(Request.Files["file" + fileOrder].FileName != null /*   .ContentLength > 0*/) 
                {
                    fileToUpload = Request.Files["file" + fileOrder];
                    try
                    {
                        if (fileToUpload.ContentLength == 0)
                        {
                            break;
                        }
                    }
                    catch (Exception) {
                        areFilesUploaded = true;
                        break;
                    }


                    //cesty k souborům
                    string fileDir = "../Web_Data/status_files/";
                    string fileFullPath = Path.Combine(HttpContext.Server.MapPath(fileDir + userId), Path.GetFileName(fileToUpload.FileName));
                    string fileDirPath = Path.GetDirectoryName(fileFullPath);

                    if (System.IO.File.Exists(fileFullPath) == true)
                    {
                        ModelState.AddModelError("file", "This file " + fileToUpload.FileName + " is already exist.");
                    }

                    if (Directory.Exists(fileDirPath) == false)
                    {
                        try
                        {
                            //pokusíme se vytvořit adresář
                            Directory.CreateDirectory(fileDirPath);
                        }
                        //jednotlivě odchytávám chyby
                        catch (UnauthorizedAccessException)
                        {
                            ModelState.AddModelError("file", "You have not permission to save file");
                        }
                        catch (Exception)
                        {
                            ModelState.AddModelError("file", "Some uknown error");
                        }
                    }

                    //ke statusu se postupně ukládají informace ohledně uploadu
                    //jestli se opravdu uploadujou soubory se rozhodne na konci metody
                    Felbook.Models.File newFile = new Felbook.Models.File();
                    if (String.IsNullOrEmpty(collection["filedescription" + fileOrder]))
                    {
                        newFile.Description = "no comment"; //pokud uživatel nevyplnil description
                    }
                    else
                    {
                        newFile.Description = collection["filedescription" + fileOrder];
                    }
                    newFile.FileName = fileToUpload.FileName;               
                    status.Files.Add(newFile);
                    filesToSave.Add(fileToUpload);
                    filesPathsToSave.Add(fileFullPath);
                    fileOrder++;
                } //konec uploadování filů


                areFilesUploaded = false;

                HttpPostedFileBase imgToUpload = null;
                /*if (Request.Files["picture" + pictureOrder].ContentLength > 0)
                {*/
                while(areFilesUploaded==false){

                    imgToUpload = Request.Files["picture" + pictureOrder];               
                    
                    try
                    {
                        if (imgToUpload.ContentLength == 0)
                        {
                            break;
                        }
                    }
                    catch (Exception) {
                        areFilesUploaded = true;
                        break;
                    }



                    //ověření jestli je to obrázek
                    if (imgToUpload.ContentType == "image/jpeg" && (imgToUpload.FileName.ToLower().EndsWith(".jpg") || imgToUpload.FileName.ToLower().EndsWith(".jpeg"))
                        || imgToUpload.ContentType == "image/gif" && imgToUpload.FileName.ToLower().EndsWith(".gif")
                        || imgToUpload.ContentType == "image/png" && imgToUpload.FileName.ToLower().EndsWith(".png")
                        )
                    {
                        //cesty k obrázkům
                        string imgDir = "../Web_Data/status_images/";
                        string imgFullPath = Path.Combine(HttpContext.Server.MapPath(imgDir + userId), Path.GetFileName(imgToUpload.FileName));
                        string imgDirPath = Path.GetDirectoryName(imgFullPath);

                        if (System.IO.File.Exists(imgFullPath) == true)
                        {
                            ModelState.AddModelError("picture", "This file " + imgToUpload.FileName + " is already exist.");
                        }

                        if (Directory.Exists(imgDirPath) == false)
                        {
                            try
                            {
                                //pokusíme se vytvořit adresář
                                Directory.CreateDirectory(imgDirPath);
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
                            newImg.FileName = imgToUpload.FileName;
                            status.Images.Add(newImg);
                            imagesToSave.Add(imgToUpload);
                            imagesPathsToSave.Add(imgFullPath);
                    }
                    else
                    {
                        ModelState.AddModelError("picture", "This file " + imgToUpload.FileName + " is not image.");
                    }
                    pictureOrder++; //inkrementace pro načtení dalšího uploadvatelného obrázku
                }
                
           // }
              
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
                int imgPointer = 0; //ukazatel abych ukazoval vzdy na spravnou cestu k obrázku
                foreach (HttpPostedFileBase img in imagesToSave)
                { //projdou ve vsechny soubory a uploadujou se
                    this.ImageResize(img, imagesPathsToSave.ElementAt(imgPointer));
                    imgPointer++;
                }
                int filePointer = 0; //ukazatel abych ukazoval vzdy na spravnou cestu k souboru
                foreach(HttpPostedFileBase uploadFile in filesToSave)
                {
                    uploadFile.SaveAs(filesPathsToSave.ElementAt(filePointer));
                    filePointer++;
                }
                Model.UserService.AddStatus(actualUser, status); //uloží se status i s obrázkem
                return RedirectToAction("Index", new { username = User.Identity.Name });
            }
        }

    }
}
