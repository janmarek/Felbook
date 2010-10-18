using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Felbook.Models
{
    public class FelbookAccountMembershipService : IMembershipService
    {

        #region Properties
        public int MinPasswordLength 
        {
            get
            {
                return 3;
            }
        }
        #endregion

        #region Methods
        public bool ValidateUser(string userName, string password)
        {
            
            if (String.IsNullOrEmpty(userName)) throw new ArgumentException("Value cannot be null or empty.", "userName");
            if (String.IsNullOrEmpty(password)) throw new ArgumentException("Value cannot be null or empty.", "password");

            //Felbook.Models.FelbookDataContext db = new Models.FelbookDataContext();

            //User user = db.Users.Single(u => u.Username == userName);

            //if (user.PassHash == password)
            //{
            //    return true;
            //}
            //else
            //{
            //    return false;
            //} 
            return false;

        }

        public MembershipCreateStatus CreateUser(string userName, string password, string email)
        {

            if (String.IsNullOrEmpty(userName)) throw new ArgumentException("Value cannot be null or empty.", "userName");
            if (String.IsNullOrEmpty(password)) throw new ArgumentException("Value cannot be null or empty.", "password");
            if (String.IsNullOrEmpty(email)) throw new ArgumentException("Value cannot be null or empty.", "email");

            return MembershipCreateStatus.Success;

        }

        public bool ChangePassword(string userName, string oldPassword, string newPassword)
        {

            if (String.IsNullOrEmpty(userName)) throw new ArgumentException("Value cannot be null or empty.", "userName");
            if (String.IsNullOrEmpty(oldPassword)) throw new ArgumentException("Value cannot be null or empty.", "oldPassword");
            if (String.IsNullOrEmpty(newPassword)) throw new ArgumentException("Value cannot be null or empty.", "newPassword");

            return false;

        }
        #endregion

    }
}