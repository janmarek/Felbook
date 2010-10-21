
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
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Felbook.Models;
using System.Web.Security;
using System.Data.SqlClient;
using System.Data.EntityClient;
using System.Data.Objects;

namespace Felbook.Tests.Models
{
    [TestClass]
    public class FelbookAccountModelTest
    {

        #region ValidateUserTests
        [TestMethod]
        public void ValidateUserTest1()
        {

            FelBookDBEntities db = new FelBookDBEntities();
            IMembershipService MembershipService = new FelbookAccountMembershipService(db);

            #region usernameTests
            try
            {
                MembershipService.ValidateUser(null, "some password");
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual("Value cannot be null or empty." + Environment.NewLine + "Parameter name: userName", ex.Message);
            }

            try
            {
                MembershipService.ValidateUser("", "some password");
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual("Value cannot be null or empty." + Environment.NewLine + "Parameter name: userName", ex.Message);
            }
            #endregion

            #region passwordTests
            try
            {
                MembershipService.ValidateUser("some username", null);
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual("Value cannot be null or empty." + Environment.NewLine + "Parameter name: password", ex.Message);
            }

            try
            {
                MembershipService.ValidateUser("some username", "");
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual("Value cannot be null or empty." + Environment.NewLine + "Parameter name: password", ex.Message);
            }
            #endregion

            try
            {
                MembershipService.ValidateUser("some username", "some password");
            }
            catch (ArgumentException)
            {
                Assert.Fail();
            }

        }

        [TestMethod]
        public void ValidateUserTest2()
        {

            FelBookDBEntities db = new FelBookDBEntities();
            IMembershipService MembershipService = new FelbookAccountMembershipService(db);
            bool result;
                        
            // TODO - vytvoření uživatele "good user" s heslem "good passwod"
            // pokud existuje, tak je zálohován původní a nahrazen testovacím
            User user = User.CreateUser(0, "Ota", "Sandr", 
                DateTime.Now, DateTime.Now, "mail", "good user",
                FelbookAccountMembershipService.CalculateSHA1WithSalt("good password", "good user"));
            db.UserSet.AddObject(user);
            db.SaveChanges();
                         
            result = MembershipService.ValidateUser("good user", "good password");
            Assert.IsTrue(result);

            result = MembershipService.ValidateUser("good user", "bad password");
            Assert.IsFalse(result);

            db.UserSet.DeleteObject(user);
            db.SaveChanges();

            // TODO - odstranění uživatele "good user" a vrácení původního, byl-li tam 
            // TODO - pokud existuje uživatel "bad user", tak se vyjme z DB

            result = MembershipService.ValidateUser("bad user", "bad password");
            Assert.IsFalse(result);

            // TODO - pokud existoval předtím uživatel "bad user", tak ho tam vrať
        }
        #endregion

        #region CreateUserTests
        [TestMethod]
        public void CreateUserTest1()
        {

            FelBookDBEntities db = new FelBookDBEntities();
            IMembershipService MembershipService = new FelbookAccountMembershipService(db);

            #region usernameTests
            try
            {
                MembershipService.CreateUser(null, "some password", "some email");
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual("Value cannot be null or empty." + Environment.NewLine + "Parameter name: userName", ex.Message);
            }

            try
            {
                MembershipService.CreateUser("", "some password", "some email");
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual("Value cannot be null or empty." + Environment.NewLine + "Parameter name: userName", ex.Message);
            }
            #endregion

            #region passwordTests
            try
            {
                MembershipService.CreateUser("some username", null, "some email");
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual("Value cannot be null or empty." + Environment.NewLine + "Parameter name: password", ex.Message);
            }

            try
            {
                MembershipService.CreateUser("some username", "", "some email");
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual("Value cannot be null or empty." + Environment.NewLine + "Parameter name: password", ex.Message);
            }
            #endregion

            #region emailTests
            try
            {
                MembershipService.CreateUser("some username", "some password", null);
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual("Value cannot be null or empty." + Environment.NewLine + "Parameter name: email", ex.Message);
            }

            try
            {
                MembershipService.CreateUser("some username", "some password", "");
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual("Value cannot be null or empty." + Environment.NewLine + "Parameter name: email", ex.Message);
            }
            #endregion

            try
            {
                MembershipService.CreateUser("some username", "some password", "some email");
                User user = db.UserSet.Single(u => u.Username == "some username");
                db.UserSet.DeleteObject(user);
                db.SaveChanges();
            }
            catch (ArgumentException)
            {
                Assert.Fail();
            }

        }

        [TestMethod]
        public void CreateUserTest2()
        {

            FelBookDBEntities db = new FelBookDBEntities();
            IMembershipService MembershipService = new FelbookAccountMembershipService(db);
            MembershipCreateStatus result;

            // vytvoření duplicateUser
            User user = User.CreateUser(0, "Ota", "Sandr",
                DateTime.Now, DateTime.Now, "mail", "duplicateUser",
                FelbookAccountMembershipService.CalculateSHA1WithSalt("good password", "duplicateUser"));
            db.UserSet.AddObject(user);
            db.SaveChanges();

            result = MembershipService.CreateUser("duplicateUser", "password", "email@provider.xy");
            Assert.AreEqual(MembershipCreateStatus.DuplicateUserName, result);

            // smazání duplicateUser
            db.UserSet.DeleteObject(user);
            db.SaveChanges();

            // zálohování normalUser
            
            result = MembershipService.CreateUser("normalUser", "password", "email@provider.xy");
            Assert.AreEqual(MembershipCreateStatus.Success, result);

            // smazání normalUser a případná obnova zálohy
            User user2 = db.UserSet.Single(u => u.Username == "normalUser");
            db.UserSet.DeleteObject(user2);
            db.SaveChanges();

        }
        #endregion

        #region ChangePasswordTests
        [TestMethod]
        public void ChangePasswordTest1()
        {
            FelBookDBEntities db = new FelBookDBEntities();
            IMembershipService MembershipService = new FelbookAccountMembershipService(db);

            #region usernameTests
            try
            {
                MembershipService.ChangePassword(null, "old password", "new password");
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual("Value cannot be null or empty." + Environment.NewLine + "Parameter name: userName", ex.Message);
            }

            try
            {
                MembershipService.ChangePassword("", "old password", "new password");
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual("Value cannot be null or empty." + Environment.NewLine + "Parameter name: userName", ex.Message);
            }
            #endregion

            #region oldPasswordTests
            try
            {
                MembershipService.ChangePassword("some username", null, "new password");
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual("Value cannot be null or empty." + Environment.NewLine + "Parameter name: oldPassword", ex.Message);
            }

            try
            {
                MembershipService.ChangePassword("some username", "", "new password");
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual("Value cannot be null or empty." + Environment.NewLine + "Parameter name: oldPassword", ex.Message);
            }
            #endregion

            #region newPasswordTests
            try
            {
                MembershipService.ChangePassword("some username", "old password", null);
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual("Value cannot be null or empty." + Environment.NewLine + "Parameter name: newPassword", ex.Message);
            }

            try
            {
                MembershipService.ChangePassword("some username", "old password", "");
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual("Value cannot be null or empty." + Environment.NewLine + "Parameter name: newPassword", ex.Message);
            }
            #endregion

            try
            {
                MembershipService.ChangePassword("some username", "old password", "new password");
            }
            catch (ArgumentException)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void ChangePasswordTest2()
        {

            FelBookDBEntities db = new FelBookDBEntities();
            IMembershipService MembershipService = new FelbookAccountMembershipService(db);
            bool result;

            // záloha badUser

            result = MembershipService.ChangePassword("badUser", "old password", "new password");
            Assert.IsFalse(result);

            // případné obnovení badUser

            // vytvoření someUser s goodPassword
            User user = User.CreateUser(0, "Ota", "Sandr",
                DateTime.Now, DateTime.Now, "mail", "someUser",
                FelbookAccountMembershipService.CalculateSHA1WithSalt("goodPassword", "someUser"));
            db.UserSet.AddObject(user);
            db.SaveChanges();

            result = MembershipService.ChangePassword("someUser", "bad password", "new password");
            Assert.IsFalse(result);

            result = MembershipService.ChangePassword("someUser", "goodPassword", "new password");
            Assert.IsTrue(result);

            // odstranění someUser
            db.UserSet.DeleteObject(user);
            db.SaveChanges();

        }
        #endregion
    }
}
