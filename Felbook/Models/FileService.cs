using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Felbook.Models
{
	public interface IFileService
	{
		string FileDir { get; set; }
		string GetFileHref(File file);
		string GetFilePath(File file);
		void SaveFile(File file, System.Web.HttpPostedFileBase uploadedFile);
	}

	public class FileService : IFileService
	{
        #region Proměnné
        private FelBookDBEntities db;
        #endregion

		#region Properties
		public string FileDir { get; set; }
		#endregion

		#region Konstruktor

		public FileService(FelBookDBEntities DBEntities)
		{
			db = DBEntities;
		}

		#endregion

		/// <summary>
		/// Disková cesta k souboru
		/// </summary>
		/// <param name="image">entita souboru</param>
		/// <returns></returns>
		public string GetFilePath(File file)
		{
			return HttpContext.Current.Server.MapPath("~" + FileDir + "/" + file.Id + "-" + file.FileName);
		}



		/// <summary>
		/// Webová cesta k souboru
		/// </summary>
		/// <param name="image">entita souboru</param>
		/// <returns></returns>
		public string GetFileHref(File file)
		{
			return FileDir + "/" + file.Id + "-" + file.FileName;
		}

		public void SaveFile(File file, HttpPostedFileBase uploadedFile)
		{
			uploadedFile.SaveAs(GetFilePath(file));
		}
	}
}
