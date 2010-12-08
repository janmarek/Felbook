using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.IO;
using Felbook;

namespace Felbook.Helpers
{
    public class ImageHelper
    {

        public static bool IsImage(string contentType)
        {
			return contentType == "image/jpeg" || contentType == "image/gif" || contentType == "image/png";
        }

		/// DEPRECATED
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

                string strExtension = Path.GetExtension(file.FileName);
                if (strExtension.ToUpper() == ".JPG" || strExtension.ToUpper() == ".GIF" || strExtension.ToUpper() == ".PNG")
                {
                    // změní velikost obrázku
                    Image imageToBeResized = Image.FromStream(file.InputStream);
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

		public static Image Resize(Image image, int maxWidth, int maxHeight)
		{
			var dimensions = CalculateSize(image.Width, image.Height, maxWidth, maxHeight);
			return new Bitmap(image, dimensions.Width, dimensions.Height);
		}

		public static Dimensions CalculateSize(int width, int height, int maxWidth, int maxHeight)
		{
			if (width > maxWidth || height > maxHeight)
			{
				height = (height * maxWidth) / width;
				width = maxWidth;

				if (height > maxHeight)
				{
					width = (width * maxHeight) / height;
					height = maxHeight;
				}
			}

			return new Dimensions {
				Width = width,
				Height = height,
			};
		}
    }

	public class Dimensions
	{
		public int Width { get; set; }
		public int Height { get; set; }
	}

	public class ImageOutputHelper
	{
		private Models.IImageService imageService;

		public ImageOutputHelper(Models.IImageService imageService)
		{
			this.imageService = imageService;
		}

		public string GetHtml(Models.Image image, string rel = null)
		{
			return "<a href=\"" + imageService.GetImageSrc(image) + "\" target=\"_blank\" class=\"colorbox\"" +
				(rel != null ? " rel=\"" + rel + "\"" : "") + ">" +
				"<img src=\"" + imageService.GetThumbnailSrc(image) + "\" " +
				"width=\"" + imageService.GetImageThumbnailWidth(image) + "\" " +
				"height=\"" + imageService.GetImageThumbnailHeight(image) + "\">" +
			"</a>";
		}
	}
}