
/*
 * Copyright (c) 2010 Ota Sandr
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Security.Cryptography;
using System.Text;

namespace Felbook.Models
{

    public interface IFelbookMembershipService : IMembershipService
    {

        MembershipCreateStatus CreateUser(RegisterModel model);

    }
    
    public class FelbookAccountMembershipService : IFelbookMembershipService
    {

        #region Variables
        private FelBookDBEntities DBEntities;
        #endregion

        #region Properties
        public int MinPasswordLength 
        {
            get
            {
                return 6;
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

				return user.CheckPassword(password);

            }
            catch (InvalidOperationException)
            {
                return false;
            }
            
        }

        public MembershipCreateStatus CreateUser(RegisterModel model)
        {
            
            string userName = model.UserName;
            string password = model.Password;
            string email = model.Email;
            
            if (String.IsNullOrEmpty(userName)) throw new ArgumentException("Value cannot be null or empty.", "userName");
            if (String.IsNullOrEmpty(password)) throw new ArgumentException("Value cannot be null or empty.", "password");
            if (String.IsNullOrEmpty(email)) throw new ArgumentException("Value cannot be null or empty.", "email");

            if (DBEntities.UserSet.Count(u => u.Username == userName) > 0)
            {
                return MembershipCreateStatus.DuplicateUserName;
            }

            User newUser = new User();
            newUser.Username = userName;
			newUser.ChangePassword(password);
            newUser.Mail = email;
            newUser.Name = model.Name;
            newUser.Surname = model.Surname;
            newUser.Created = DateTime.Now;
            newUser.LastLogged = DateTime.Now;
            newUser.ICQ = model.ICQ;
            newUser.Phone = model.Phone;
            newUser.SchoolEmail = model.SchoolEmail;
            newUser.Specialization = model.Specialization;
            newUser.Title = model.Title;
            newUser.StudyProgramme = model.StudyProgramme;
            newUser.TitleAfter = model.TitleAfter;

            DBEntities.UserSet.AddObject(newUser);
            DBEntities.SaveChanges();

            return MembershipCreateStatus.Success;
            // TODO spojit tyto dvě metody
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
			newUser.ChangePassword(password);
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

                if (user.CheckPassword(oldPassword))
                {
                    user.ChangePassword(newPassword);
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

        #endregion

    }
}