using Felbook.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using System.Collections.Generic;

namespace Felbook.Tests
{
    
    
    /// <summary>
    ///This is a test class for MessageModelTest and is intended
    ///to contain all MessageModelTest Unit Tests
    ///</summary>
    [TestClass()]
    public class MessageModelTest
    {
        
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
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for MessageModel Constructor
        ///</summary>
        [TestMethod()]
        public void MessageModelConstructorTest()
        {
            MessageModel target = new MessageModel();
            Assert.IsNotNull(target);
        }

        /// <summary>
        ///A test for GetMessageById
        ///</summary>
        [TestMethod()]
        public void GetMessageByIdTest()
        {
            FelBookDBEntities DbEntities = new FelBookDBEntities();
            MessageModel target = new MessageModel();

            int ID = 0; 
            Message expected = null; 
            Message actual = target.GetMessageById(ID);
            Assert.AreEqual(expected, actual);

            expected = new Message();
            User mockUser = User.CreateUser(0, "test", "test",
                DateTime.Now, DateTime.Now, "mail", "test user", "");

            expected.Users.Add(mockUser);
            expected.Sender = mockUser;
            expected.Text = "text";
            expected.Created = DateTime.Now;

            DbEntities.MessageSet.AddObject(expected);
            DbEntities.SaveChanges();
            ID = expected.Id;
            actual = target.GetMessageById(ID);
            Assert.AreEqual(expected.Text, actual.Text);
            
            DbEntities.MessageSet.DeleteObject(expected);
            DbEntities.UserSet.DeleteObject(mockUser);
            DbEntities.SaveChanges();
        }

        /// <summary>
        ///A test for GetMessagesSentByUser
        ///</summary>
        [TestMethod()]
        public void GetMessagesSentByUserTest()
        {
            FelBookDBEntities DbEntities = new FelBookDBEntities();
            MessageModel target = new MessageModel();
            User mockUser = User.CreateUser(0, "test", "test",
                DateTime.Now, DateTime.Now, "mail", "test user", "");
            DbEntities.UserSet.AddObject(mockUser);
            DbEntities.SaveChanges();
            string username = "test user";
            
            List<Message> actual = target.GetMessagesSentByUser(username);
            Assert.IsTrue(actual.Count == 0);

            DbEntities.UserSet.DeleteObject(mockUser);
            DbEntities.SaveChanges();
            
        }

        /// <summary>
        ///A test for SendMessageToUsers
        ///</summary>
        //[TestMethod()]
        public void SendMessageToUsersTest()
        {
            MessageModel target = new MessageModel(); // TODO: Initialize to an appropriate value
            string sender = string.Empty; // TODO: Initialize to an appropriate value
            List<string> recievers = null; // TODO: Initialize to an appropriate value
            int prevMessageID = 0; // TODO: Initialize to an appropriate value
            string text = string.Empty; // TODO: Initialize to an appropriate value
            target.SendMessageToUsers(sender, recievers, prevMessageID, text);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }
    }
}
