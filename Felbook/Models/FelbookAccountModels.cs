using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Security.Cryptography;
using System.Text;

namespace Felbook.Models
{
    public class FelbookAccountMembershipService : IMembershipService
    {

        #region Variables
        private FelBookDBEntities DBEntities;
        #endregion

        #region Properties
        public int MinPasswordLength 
        {
            get
            {
                return 3;
            }
        }
        #endregion

        #region Constructors
        public FelbookAccountMembershipService(FelBookDBEntities db)
        {
            DBEntities = db;
        }
        #endregion

        #region Methods
        public bool ValidateUser(string userName, string password)
        {
            
            if (String.IsNullOrEmpty(userName)) throw new ArgumentException("Value cannot be null or empty.", "userName");
            if (String.IsNullOrEmpty(password)) throw new ArgumentException("Value cannot be null or empty.", "password");

            try
            {
                User user = DBEntities.UserSet.Single(u => u.Username == userName);

                string hash = CalculateSHA1WithSalt(password, userName);

                if (user.PasswordHash == hash)
                {
                    return true;
                }
                else
                {
                    return false;
                } 

            }
            catch (InvalidOperationException)
            {
                return false;
            }
            
        }

        public MembershipCreateStatus CreateUser(string userName, string password, string email)
        {

            if (String.IsNullOrEmpty(userName)) throw new ArgumentException("Value cannot be null or empty.", "userName");
            if (String.IsNullOrEmpty(password)) throw new ArgumentException("Value cannot be null or empty.", "password");
            if (String.IsNullOrEmpty(email)) throw new ArgumentException("Value cannot be null or empty.", "email");

            if (DBEntities.UserSet.Count(u => u.Username == userName) > 0)
            {
                return MembershipCreateStatus.DuplicateUserName;
            }

            User newUser = new User();
            newUser.Username = userName;
            newUser.PasswordHash = CalculateSHA1WithSalt(password, userName);
            newUser.Mail = email;
            newUser.Name = "";
            newUser.Surname = "";
            newUser.Created = DateTime.Now;
            newUser.LastLogged = DateTime.Now;

            DBEntities.UserSet.AddObject(newUser);
            DBEntities.SaveChanges();

            return MembershipCreateStatus.Success;

        }

        public bool ChangePassword(string userName, string oldPassword, string newPassword)
        {

            if (String.IsNullOrEmpty(userName)) throw new ArgumentException("Value cannot be null or empty.", "userName");
            if (String.IsNullOrEmpty(oldPassword)) throw new ArgumentException("Value cannot be null or empty.", "oldPassword");
            if (String.IsNullOrEmpty(newPassword)) throw new ArgumentException("Value cannot be null or empty.", "newPassword");

            try
            {
                User user = DBEntities.UserSet.Single(u => u.Username == userName);

                string hash = CalculateSHA1WithSalt(oldPassword, userName);

                if (user.PasswordHash == hash)
                {
                    user.PasswordHash = CalculateSHA1WithSalt(newPassword, userName);
                    DBEntities.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                } 

            }
            catch (InvalidOperationException)
            {
                return false;
            }

        }

        public static string CalculateSHA1WithSalt(string text, string salt)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(text + salt);
            SHA1CryptoServiceProvider cryptoTransformSHA1 = new SHA1CryptoServiceProvider();
            string hash = BitConverter.ToString(cryptoTransformSHA1.ComputeHash(buffer)).Replace("-", "");

            return hash;
        }

        #endregion

    }
}