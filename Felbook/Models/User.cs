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

    [MetadataType(typeof(UserMetadata))]
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

        public string Password { 
            get {return this.password;}
            set { this.password = value;} 
        }


        public string ConfirmPassword {
            get { return this.confirmPassword;}
            set { this.confirmPassword = value;} 
        }
    }
   
    #region Validace
    public class UserMetadata
    {
        [ICQ(ErrorMessage = "ICQ is not valid.")]
        [DisplayName("ICQ")]
        public string ICQ { get; set; }

        [Email(ErrorMessage = "School email is not valid.")]
        [DisplayName("School email address")]
        public string SchoolEmail { get; set; }

        [ICQ(ErrorMessage = "Phone is not valid.")]
        [DisplayName("Phone")]
        public string Phone { get; set; }

    }
    #endregion

}