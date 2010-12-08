using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Felbook.Models
{
	public class StatusException : Exception
	{
		public StatusException(string message)
			: base(message)
		{
		}
	}
}