using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Felbook.Models;

namespace Felbook.Helpers
{
	public class FileOutputHelper
	{
		private IFileService service;

		public FileOutputHelper(IFileService fileService)
		{
			service = fileService;
		}

		public string GetHtml(File file)
		{
			return "<a href=\"" + service.GetFileHref(file) + "\" target=\"_blank\">" + file.FileName + "</a>";
		}
	}
}