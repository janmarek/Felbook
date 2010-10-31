﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Security.Cryptography;

namespace Felbook.Models
{
	public partial class User
	{
		/// <summary>
		/// Jméno a příjmení
		/// </summary>
		public string FullName
		{
			get
			{
				return Name + " " + Surname;
			}
		}

		/// <summary>
		/// Získat osolený hash hesla
		/// </summary>
		/// <param name="password">heslo</param>
		/// <returns></returns>
		private string CalculateHash(string password)
		{
			byte[] buffer = Encoding.UTF8.GetBytes(password + Username);
			SHA1CryptoServiceProvider cryptoTransformSHA1 = new SHA1CryptoServiceProvider();
			return BitConverter.ToString(cryptoTransformSHA1.ComputeHash(buffer)).Replace("-", "");
		}


		/// <summary>
		/// Změnit heslo
		/// </summary>
		/// <param name="value">heslo</param>
		public void ChangePassword(string value)
		{
			PasswordHash = CalculateHash(value);
		}


		/// <summary>
		/// Ověřit heslo
		/// </summary>
		/// <param name="password">heslo</param>
		/// <returns>heslo je platné</returns>
		public bool CheckPassword(string password)
		{
			return CalculateHash(password) == PasswordHash;
		}

	}
}