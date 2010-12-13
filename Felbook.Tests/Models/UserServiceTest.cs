using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Felbook.Models;

namespace Felbook.Tests.Models
{
    /// <summary>
    /// Summary description for UserServiceTest1
    /// </summary>
    [TestClass]
    public class UserServiceTest
    {
        #region Proměnnné
        FelBookDBEntities db;
        WallService wallServ;
        UserService userService;
        #endregion

        #region Kontruktor

        public UserServiceTest()
        {
            db = new FelBookDBEntities();
            wallServ = new WallService(db);
            userService = new UserService(db, wallServ);
        }
        #endregion

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        #region Testovací metody

        /// <summary>
        /// Testování konstruktoru UserService
        /// </summary>
        [TestMethod]
        public void UserServiceConstructorTest()
        {
            db = new FelBookDBEntities();
            wallServ = new WallService(db);
            userService = new UserService(db, wallServ);
            Assert.IsNotNull(userService);
        }
        
        /// <summary>
        /// Testování jestli je user followován
        /// </summary>
        [TestMethod]
        public void FollowUserTest() 
        {
            User user1 = new User();
            User user2 = new User();
            Assert.AreEqual(0, user1.Followers.Count);
            userService.FollowUser(user1, user2);
            Assert.AreEqual(1, user1.Followers.Count);
            Assert.AreEqual(user2, user1.Followers.ElementAt(0));
        }

        /// <summary>
        /// Testování jestli se nalezne uživatel podle jména
        /// </summary>
        [TestMethod]
        public void FindByUsernameTest()
        {        
            User expected = null;
            User actual = userService.FindByUsername("90909090");
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Testování jestli se správně vypisuje vlastnost FullName
        /// </summary>
        [TestMethod]
        public void FullNameTest()
        {
            String firstname = "Jakub";
            String surname = "Novák";
            User user1 = new User();
            user1.Name = "Jakub";
            user1.Surname = "Novák";
            Assert.AreEqual(user1.FullName, firstname + " " + surname);
        }

        /// <summary>
        /// Testování jeslti se správně změnilo heslo
        /// </summary>
        [TestMethod]
        public void ChangePasswordTest() 
        {
            User user1 = new User();
            user1.Name = "Jan";
            user1.Surname = "Novák";
            user1.ChangePassword("Nové heslo 123456987");
            Assert.IsFalse(user1.CheckPassword("špatné heslo"));
            Assert.IsTrue(user1.CheckPassword("Nové heslo 123456987"));
        }

        /// <summary>
        /// Testování metody která vrácí jestli je uživatel followován
        /// </summary>
        [TestMethod]
        public void IsFollowedByTest() 
        {
            User user1 = new User();
            User user2 = new User();
            Assert.IsFalse(user1.IsFollowedBy(user2));
            userService.FollowUser(user1, user2);
            Assert.IsTrue(user1.IsFollowedBy(user2));
        }
        #endregion
    }
}
