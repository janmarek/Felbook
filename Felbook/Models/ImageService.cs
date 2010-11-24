using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using Felbook.Helpers;

namespace Felbook.Models
{
	public interface IImageService
	{
		string GetImagePath(Image image);
		string GetImageSrc(Image image);
		int GetImageThumbnailHeight(Image image);
		int GetImageThumbnailWidth(Image image);
		string GetThumbnailPath(Image image);
		string GetThumbnailSrc(Image image);
		string ImageDir { get; set; }
		int MaxHeight { get; set; }
		int MaxThumbnailHeight { get; set; }
		int MaxThumbnailWidth { get; set; }
		int MaxWidth { get; set; }
		void SaveImage(Image image, System.IO.Stream inputStream);
	}

	public class ImageService : IImageService
	{
        #region Proměnné
        private FelBookDBEntities db;
        #endregion

		#region Properties

		public string ImageDir { get; set; }
		public int MaxWidth { get; set; }
		public int MaxHeight { get; set; }
		public int MaxThumbnailWidth { get; set; }
		public int MaxThumbnailHeight { get; set; }

		#endregion

		#region Konstruktor

		public ImageService(FelBookDBEntities DBEntities)
		{
			db = DBEntities;
		}

		#endregion

		/// <summary>
		/// Disková cesta k obrázku
		/// </summary>
		/// <param name="image">entita obrázku</param>
		/// <returns></returns>
		public string GetImagePath(Image image)
		{
			return HttpContext.Current.Server.MapPath("~" + ImageDir + "/" + image.Id + ".jpg");
		}



		/// <summary>
		/// Webová cesta k obrázku
		/// </summary>
		/// <param name="image">entita obrázku</param>
		/// <returns></returns>
		public string GetImageSrc(Image image)
		{
			return ImageDir + "/" + image.Id + ".jpg";
		}



		/// <summary>
		/// Disková cesta k náhledu
		/// </summary>
		/// <param name="image">entita obrázku</param>
		/// <returns></returns>
		public string GetThumbnailPath(Image image)
		{
			return HttpContext.Current.Server.MapPath("~" + ImageDir + "/" + image.Id + "-thumb.jpg");
		}



		/// <summary>
		/// Webová cesta k náhledu
		/// </summary>
		/// <param name="image">entita obrázku</param>
		/// <returns></returns>
		public string GetThumbnailSrc(Image image)
		{
			return ImageDir + "/" + image.Id + "-thumb.jpg";
		}



		/// <summary>
		/// Uložit obrázek a náhled
		/// </summary>
		/// <param name="image">entita obrázku</param>
		/// <param name="inputStream">input stream s obrázkem</param>
		public void SaveImage(Image image, Stream inputStream)
		{
			var img = ImageHelper.Resize(System.Drawing.Image.FromStream(inputStream), MaxWidth, MaxHeight);
			var thumb = ImageHelper.Resize(img, MaxThumbnailWidth, MaxThumbnailHeight);

			var path = GetImagePath(image);
			img.Save(path, System.Drawing.Imaging.ImageFormat.Jpeg);
			var thumbPath = GetThumbnailPath(image);
			thumb.Save(thumbPath, System.Drawing.Imaging.ImageFormat.Jpeg);
		}



		/// <summary>
		/// Šířka náhledu v px
		/// </summary>
		/// <param name="image">entita obrázku</param>
		/// <returns>šířka</returns>
		public int GetImageThumbnailWidth(Image image)
		{
			// TODO nenačítat kvůli takové pitomině celý obrázek do paměti
			return new System.Drawing.Bitmap(GetThumbnailPath(image)).Width;
		}



		/// <summary>
		/// Výška náhledu v px
		/// </summary>
		/// <param name="image">entita obrázku</param>
		/// <returns>výška</returns>
		public int GetImageThumbnailHeight(Image image)
		{
			// TODO nenačítat kvůli takové pitomině celý obrázek do paměti
			return new System.Drawing.Bitmap(GetThumbnailPath(image)).Height;
		}
	}
}