using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;

namespace Felbook.Helpers
{
    public class Image
    {

        public bool IsImage(HttpPostedFileBase imgToUpload)
        {
            if (imgToUpload.ContentType == "image/jpeg" && (imgToUpload.FileName.ToLower().EndsWith(".jpg") || imgToUpload.FileName.ToLower().EndsWith(".jpeg"))
                        || imgToUpload.ContentType == "image/gif" && imgToUpload.FileName.ToLower().EndsWith(".gif")
                        || imgToUpload.ContentType == "image/png" && imgToUpload.FileName.ToLower().EndsWith(".png")
                        )
            {
                return true;
            }
            else 
            {
                return false;
            }
        }

        /// <summary>
        /// Metoda která změní velikost obrázku a rovnou ho uploaduje
        /// </summary>
        /// <param name="file">Fyzicky reprezentovaný soubor</param>
        /// <param name="filePath">Cesta k souboru</param>
        /// <param name="maximumWidth">Maximální šířka podle které se dyžtak ořízne</param>
        /// <param name="maximumHeight">Maximální výška podle které se dyžtak ořízne</param>
        public void ImageResize(HttpPostedFileBase file, string filePath, int maximumWidth, int maximumHeight)
        {
            if (file != null && file.FileName != "")
            {
                int maxWidth = maximumWidth; //maximální šířka
                int maxHeight = maximumHeight; //maximální výška

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
    }
}