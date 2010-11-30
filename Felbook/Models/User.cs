using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Security.Cryptography;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Felbook.Models
{

	public partial class User
	{

        #region Proměnné
        private string oldPasswrod = "";
        private string password = "";
        private string confirmPassword = "";
        #endregion

        public User()
		{
			Created = DateTime.Now;
		}

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
		public string CalculateHash(string password)
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


		/// <summary>
		/// Zjistit jestli některý uživatel sleduje uživatele
		/// </summary>
		/// <param name="follower">potenciální follower</param>
		/// <returns></returns>
		public bool IsFollowedBy(User follower)
		{
			return Followers.Contains(follower);
        }


        public string OldPassword {     
            get
            {
            return this.oldPasswrod;
            }
            set 
            {
                this.oldPasswrod = value;
            } 
        }
        #region Validace
        

        /*[ValidatePasswordLength]
        [DataType(DataType.Password)]
        [DisplayName("Password *")]*/
        public string Password { 
            get {return this.password;}
            set { this.password = value;} 
        }

        //[DataType(DataType.Password)]
        //[DisplayName("Confirm password *")]
        public string ConfirmPassword {
            get { return this.confirmPassword;}
            set { this.confirmPassword = value;} 
        }
        
        #endregion
    }

}