using Felbook.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using System.Collections.Generic;
using System.Linq;

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
            MessageService target = new MessageService();
            Assert.IsNotNull(target);
        }

        /// <summary>
        ///A test for GetMessageById
        ///</summary>
        [TestMethod()]
        public void GetMessageByIdTest()
        {
            FelBookDBEntities DbEntities = new FelBookDBEntities();
            MessageService target = new MessageService();

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
        ///A test for SendMessageToUsers
        ///</summary>
        [TestMethod()]
        public void SendMessageToUsersTest()
        {
            FelBookDBEntities DbEntities = new FelBookDBEntities();
            MessageService target = new MessageService();

            User mockSender = User.CreateUser(0, "test", "test",
                DateTime.Now, DateTime.Now, "mail", "sender", "");
            DbEntities.UserSet.AddObject(mockSender);
            User mockReciever = User.CreateUser(0, "test", "test",
                DateTime.Now, DateTime.Now, "mail", "reciever", "");
            DbEntities.UserSet.AddObject(mockReciever);
            DbEntities.SaveChanges();
            
            
            string sender = mockSender.Username; 
            List<string> recievers = new List<string>();
            recievers.Add(mockReciever.Username);
            int prevMessageID = 0;
            string text = "some text";
            target.SendMessageToUsers(sender, recievers, prevMessageID, text);

            Message message1 = DbEntities.MessageSet.Single(m => m.Sender.Username == mockSender.Username);
            Assert.AreEqual(sender, message1.Sender.Username);
            Assert.IsTrue(message1.Users.Contains(mockReciever));
            Assert.AreEqual(text, message1.Text);
            Assert.IsNull(message1.FirstMessage);

            
            sender = mockReciever.Username;
            recievers = new List<string>();
            recievers.Add(mockSender.Username);
            prevMessageID = message1.Id;
            text = "some other text";
            target.SendMessageToUsers(sender, recievers, prevMessageID, text);
           
            Message message2 = DbEntities.MessageSet.Single(m => m.Sender.Username == mockReciever.Username);
            Assert.AreEqual(sender, message2.Sender.Username);
            Assert.IsTrue(message2.Users.Contains(mockSender));
            Assert.AreEqual(text, message2.Text);
            Assert.AreEqual(message1, message2.FirstMessage);


            DbEntities.MessageSet.DeleteObject(message2);
            DbEntities.MessageSet.DeleteObject(message1);
            DbEntities.UserSet.DeleteObject(mockReciever);
            DbEntities.UserSet.DeleteObject(mockSender);
            DbEntities.SaveChanges();
            
        }
    }
}
