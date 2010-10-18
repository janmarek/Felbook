using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Felbook.Models;

namespace Felbook.Tests.Models
{
    [TestClass]
    public class FelbookAccountModelTest
    {

        #region ValidateUserTests
        [TestMethod]
        public void ValidateUserTest1()
        {

            IMembershipService MembershipService = new FelbookAccountMembershipService();

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

            IMembershipService MembershipService = new FelbookAccountMembershipService();
            bool result;

            // TODO - vytvoření uživatele "good user" s heslem "good passwod"
            // pokud existuje, tak je zálohován původní a nahrazen testovacím

            result = MembershipService.ValidateUser("good user", "good password");
            Assert.IsTrue(result);

            result = MembershipService.ValidateUser("good user", "bad password");
            Assert.IsFalse(result);

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

            IMembershipService MembershipService = new FelbookAccountMembershipService();

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
            }
            catch (ArgumentException)
            {
                Assert.Fail();
            }

        }
        #endregion

        #region ChangePasswordTests
        [TestMethod]
        public void ChangePasswordTest1()
        {

        }
        #endregion
    }
}
