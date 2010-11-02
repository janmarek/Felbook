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



namespace Felbook.Controllers
{
    public class ProfileController : Controller
    {
        #region Proměnné
        /// <summary>
        /// Znak podle kterého se budou dělit strinq linků do pole
        /// </summary>
        private char linkDelimiter = '°';
        #endregion

        public Model Model { get; set; }

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
        /// uloží linky do databáze</returns>
        [AcceptVerbs(HttpVerbs.Post), HttpPost]
        public ActionResult SetLinksContent(FormCollection collection)  
        {
            string returnVal=String.Empty; //výsledný řetězec který metoda vrací
            if (ValidLink(collection["newLink"]) != String.Empty)
            {
                Session["links"] += collection["newLink"] + linkDelimiter;
            }
            else 
            {
                returnVal += collection["newLink"] + " is not valid link.";
            }
            List<string> returnList = new List<string>();

            try
            {
                returnList = Session["links"].ToString().Split(linkDelimiter).ToList<string>();
            }
            catch (Exception e) {
                returnVal += e.Message;
            }           
            returnVal += "<ul>";
            for (int i = 0; i < (returnList.Count-1); i++) {
                returnVal += "<li>" + returnList.ElementAt(i).ToString() + "</li>";
            }
            returnVal += "</ul>";
            return Content(returnVal);  
        }

        /// <summary>
        /// Metoda pro validaci jestli daný link je validní a jestli již neni v Session s linky
        /// </summary>
        /// <param name="str">Link který validujeme</param>
        /// <returns>Vrátí stríng který reprezentuje link</returns>
        private string ValidLink(string link) {          
            //tady bude testování jestli daný link existuje skusí se to nějakou připojovací metodou přes webclienta
            List<string> linkList = new List<string>();
            /*if (String.IsNullOrEmpty(Session["links"].ToString())==false)
            {
                linkList = Session["links"].ToString().Split(linkDelimiter).ToList<string>();
            }
            //ověřuje se jestli daný link již existuje
            foreach(string link in linkList){
                if (link.Contains(str)) {
                    return String.Empty;
                }
            }*/
            //String ahoj = Session["links"].ToString();
            
            if (true)
            {
                return link;           
            }
            /*else {
                return String.Empty;
            }*/
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
            
            //representace souborů k uploadu
            List<HttpPostedFileBase> filesToSave = new List<HttpPostedFileBase>();
            //cesty kam se budou soubory ukládat
            List<string> filesPathsToSave = new List<string>();

            int fileOrder = 1; //fileOrder je proměnná určující pořadí obrázku
            while (Request.Files["picture" + fileOrder].ContentLength > 0)
            {
                HttpPostedFileBase file = Request.Files["picture" + fileOrder];
                //ověření jestli je to obrázek
                if (file.ContentLength > 0)
                {
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
                            if (String.IsNullOrEmpty(collection["description" + fileOrder]))
                            {
                                newImg.Description = "no comment"; //pokud uživatel nevyplnil description
                            }
                            else
                            {
                                newImg.Description = collection["description" + fileOrder];
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
                fileOrder++; //inkrementace pro načtení dalšího uploadvatelného obrázku
            }
              
            if (!ModelState.IsValid) //pokud model není validní tak se nic neuloží a pouze se ukážou chyby pomocí View[]
            {
                return View("Index", actualUser);
            }
            else //pokud je model validní tak uloží user linky, uploaduje a uloží obrázky a uloží samotný status k uživately
            {
                //upload linků ke statusu
                if (Session["links"]!=null)
                {  
                    List<string> userLinks = Session["links"].ToString().Split(linkDelimiter).ToList();
                    for (int i = 0; i < (userLinks.Count - 1); i++)
                    {
                        Felbook.Models.Link newLink = new Felbook.Models.Link();
                        newLink.URL = userLinks.ElementAt(i);
                        status.Links.Add(newLink);
                    }
                    Session["links"] = null; //vymaže session
                }
                //nyní upload obrázku ke statusu
                int filePointer = 0; //ukazatel abych ukazoval vzdy na spravnou cestu k souboru
                foreach (HttpPostedFileBase file in filesToSave)
                { //projdou ve vsechny soubory a uploadujou se
                    //file.SaveAs(filesPathsToSave.ElementAt(filePointer));
                    this.ImageResize(file, filesPathsToSave.ElementAt(filePointer));
                    filePointer++;
                }
                Model.UserService.AddStatus(actualUser, status); //uloží se status i s obrázkem
                return RedirectToAction("Index", new { username = User.Identity.Name });
            }
        }

    }
}
