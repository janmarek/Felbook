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
    public class MessageServiceTest
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
        ///A test for MessageService Constructor
        ///</summary>
        [TestMethod()]
        public void MessageServiceConstructorTest()
        {
            FelBookDBEntities db = new FelBookDBEntities();
            MessageService target = new MessageService(db);
            Assert.IsNotNull(target);
        }

        /// <summary>
        ///A test for FindById
        ///</summary>
        [TestMethod()]
        public void FindByIdTest()
        {
            FelBookDBEntities DbEntities = new FelBookDBEntities();
            MessageService target = new MessageService(DbEntities);

            int ID = 0; 
            Message expected = null;
            Message actual = target.FindById(ID);
            Assert.AreEqual(expected, actual);

            expected = new Message();
            User mockUser = User.CreateUser(0, "test", "test",
                DateTime.Now, DateTime.Now, "mail", "test user", "");

            expected.Recievers.Add(mockUser);
            expected.Sender = mockUser;
            expected.Text = "text";
            expected.Created = DateTime.Now;

            DbEntities.MessageSet.AddObject(expected);
            DbEntities.SaveChanges();
            ID = expected.Id;
            actual = target.FindById(ID);
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
            MessageService target = new MessageService(DbEntities);

            User mockSender = User.CreateUser(0, "test", "test",
                DateTime.Now, DateTime.Now, "mail", "sender", "");
            DbEntities.UserSet.AddObject(mockSender);
            User mockReciever = User.CreateUser(0, "test", "test",
                DateTime.Now, DateTime.Now, "mail", "reciever", "");
            DbEntities.UserSet.AddObject(mockReciever);
            DbEntities.SaveChanges();
            
            
            ISet<User> recievers = new HashSet<User>();
            recievers.Add(mockReciever);
            Message prevMessage = null;
            string text = "some text";
            target.SendMessageToUsers(mockSender, recievers, prevMessage, text);

            Message message1 = DbEntities.MessageSet.Single(m => m.Sender.Username == mockSender.Username);
            Assert.AreEqual(mockSender, message1.Sender);
            Assert.IsTrue(message1.Recievers.Contains(mockReciever));
            Assert.AreEqual(text, message1.Text);
            Assert.IsNull(message1.ReplyTo);

            
            recievers = new HashSet<User>();
            recievers.Add(mockSender);
            prevMessage = message1;
            text = "some other text";
            target.SendMessageToUsers(mockReciever, recievers, prevMessage, text);
           
            Message message2 = DbEntities.MessageSet.Single(m => m.Sender.Username == mockReciever.Username);
            Assert.AreEqual(mockReciever, message2.Sender);
            Assert.IsTrue(message2.Recievers.Contains(mockSender));
            Assert.AreEqual(text, message2.Text);
            Assert.AreEqual(message1, message2.ReplyTo);


            DbEntities.MessageSet.DeleteObject(message2);
            DbEntities.MessageSet.DeleteObject(message1);
            DbEntities.UserSet.DeleteObject(mockReciever);
            DbEntities.UserSet.DeleteObject(mockSender);
            DbEntities.SaveChanges();
            
        }

        /// <summary>
        ///A test for MarkMessageReadBy
        ///</summary>
        [TestMethod()]
        public void MarkMessageReadByTest()
        {
            FelBookDBEntities DbEntities = new FelBookDBEntities();
            MessageService target = new MessageService(DbEntities);

            User mockSender = User.CreateUser(0, "test", "test",
                DateTime.Now, DateTime.Now, "mail", "sender", "");
            DbEntities.UserSet.AddObject(mockSender);
            User mockReciever = User.CreateUser(0, "test", "test",
                DateTime.Now, DateTime.Now, "mail", "reciever", "");
            DbEntities.UserSet.AddObject(mockReciever);
            DbEntities.SaveChanges();

            Message msg1 = Message.CreateMessage(0, "Text", DateTime.Now);
            msg1.Sender = mockSender;
            msg1.Recievers.Add(mockReciever);
            DbEntities.MessageSet.AddObject(msg1);
            DbEntities.SaveChanges();


            Assert.IsFalse(mockReciever.ReadMessages.Contains(msg1));
            Assert.IsFalse(msg1.Readers.Contains(mockReciever));
            
            target.MarkMessageReadBy(msg1, mockReciever);

            Assert.IsTrue(mockReciever.ReadMessages.Contains(msg1));
            Assert.IsTrue(msg1.Readers.Contains(mockReciever));

            target.MarkMessageReadBy(msg1, mockReciever);

            Assert.IsTrue(mockReciever.ReadMessages.Contains(msg1));
            Assert.IsTrue(msg1.Readers.Contains(mockReciever));


            DbEntities.MessageSet.DeleteObject(msg1);
            DbEntities.UserSet.DeleteObject(mockReciever);
            DbEntities.UserSet.DeleteObject(mockSender);
            DbEntities.SaveChanges();
        }

        /// <summary>
        ///A test for MarkMessageUnreadBy
        ///</summary>
        [TestMethod()]
        public void MarkMessageUnreadByTest()
        {
            FelBookDBEntities DbEntities = new FelBookDBEntities();
            MessageService target = new MessageService(DbEntities);

            User mockSender = User.CreateUser(0, "test", "test",
                DateTime.Now, DateTime.Now, "mail", "sender", "");
            DbEntities.UserSet.AddObject(mockSender);
            User mockReciever = User.CreateUser(0, "test", "test",
                DateTime.Now, DateTime.Now, "mail", "reciever", "");
            DbEntities.UserSet.AddObject(mockReciever);
            DbEntities.SaveChanges();

            Message msg1 = Message.CreateMessage(0, "Text", DateTime.Now);
            msg1.Sender = mockSender;
            msg1.Recievers.Add(mockReciever);
            msg1.Readers.Add(mockReciever);
            DbEntities.MessageSet.AddObject(msg1);
            DbEntities.SaveChanges();


            Assert.IsTrue(mockReciever.ReadMessages.Contains(msg1));
            Assert.IsTrue(msg1.Readers.Contains(mockReciever));

            target.MarkMessageUnreadBy(msg1, mockReciever);

            Assert.IsFalse(mockReciever.ReadMessages.Contains(msg1));
            Assert.IsFalse(msg1.Readers.Contains(mockReciever));

            target.MarkMessageUnreadBy(msg1, mockReciever);

            Assert.IsFalse(mockReciever.ReadMessages.Contains(msg1));
            Assert.IsFalse(msg1.Readers.Contains(mockReciever));


            DbEntities.MessageSet.DeleteObject(msg1);
            DbEntities.UserSet.DeleteObject(mockReciever);
            DbEntities.UserSet.DeleteObject(mockSender);
            DbEntities.SaveChanges();
        }

        /// <summary>
        ///A test for NumberOfUnreadMessages
        ///</summary>
        [TestMethod()]
        public void NumberOfUnreadMessagesTest()
        {
            FelBookDBEntities DbEntities = new FelBookDBEntities();
            MessageService target = new MessageService(DbEntities);

            User mockSender = User.CreateUser(0, "test", "test",
                DateTime.Now, DateTime.Now, "mail", "sender", "");
            DbEntities.UserSet.AddObject(mockSender);
            User mockReciever = User.CreateUser(0, "test", "test",
                DateTime.Now, DateTime.Now, "mail", "reciever", "");
            DbEntities.UserSet.AddObject(mockReciever);
            DbEntities.SaveChanges();

            Assert.AreEqual(target.NumberOfUnreadMessages(mockReciever), 0);

            Message msg1 = Message.CreateMessage(0, "Text", DateTime.Now);
            msg1.Sender = mockSender;
            msg1.Recievers.Add(mockReciever);
            DbEntities.MessageSet.AddObject(msg1);
            DbEntities.SaveChanges();

            Assert.AreEqual(target.NumberOfUnreadMessages(mockReciever), 1);

            Message msg2 = Message.CreateMessage(0, "Text", DateTime.Now);
            msg2.Sender = mockSender;
            msg2.Recievers.Add(mockReciever);
            DbEntities.MessageSet.AddObject(msg2);
            DbEntities.SaveChanges();

            Assert.AreEqual(target.NumberOfUnreadMessages(mockReciever), 2);

            msg2.Readers.Add(mockReciever);
            DbEntities.SaveChanges();
            Assert.AreEqual(target.NumberOfUnreadMessages(mockReciever), 1);

            msg1.Readers.Add(mockReciever);
            DbEntities.SaveChanges();
            Assert.AreEqual(target.NumberOfUnreadMessages(mockReciever), 0);

            msg1.Readers.Remove(mockReciever);
            msg2.Readers.Remove(mockReciever);
            DbEntities.SaveChanges();
            Assert.AreEqual(target.NumberOfUnreadMessages(mockReciever), 2);


            DbEntities.MessageSet.DeleteObject(msg1);
            DbEntities.MessageSet.DeleteObject(msg2);
            DbEntities.UserSet.DeleteObject(mockReciever);
            DbEntities.UserSet.DeleteObject(mockSender);
            DbEntities.SaveChanges();
        }
    }
}
