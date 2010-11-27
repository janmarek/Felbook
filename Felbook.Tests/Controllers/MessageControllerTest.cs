// license MIT

using Felbook.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using Felbook.Models;
using Felbook.Tests.Fakes;
using System.Web.Mvc;
using System.Linq;
using Moq;

namespace Felbook.Tests
{
   
    /// <summary>
    ///This is a test class for MessageControllerTest and is intended
    ///to contain all MessageControllerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class MessageControllerTest
    {
        
        private TestContext testContextInstance;

        private static MockModel TestModel;

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
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            // Inicicalizace a naplnění modelu pro testy            
            TestModel = new MockModel();

            User user1 = User.CreateUser(0, "Jindra", "Hrnčír", DateTime.Now,
                  DateTime.Now, "hrncir.jindra@nebelvir.br", "hpotter", "alohomora");
            TestModel.UserList.Add(user1);

            User user2 = User.CreateUser(1, "Tomáš", "Raddle", DateTime.Now,
                DateTime.Now, "tomas.raddle@zmijozel.br", "voltmetr", "avadaKadevra");
            TestModel.UserList.Add(user2);

            Message firstMsg = Message.CreateMessage(0, "Text", DateTime.Now);
            TestModel.MessageList.Add(firstMsg);
            firstMsg.Sender = user1;
            user1.SentMessages.Add(firstMsg);
            firstMsg.Recievers.Add(user2);
            user2.Messages.Add(firstMsg);

            Message secondMsg = Message.CreateMessage(1, "Another Text", DateTime.Now);
            TestModel.MessageList.Add(secondMsg);
            secondMsg.Sender = user2;
            user2.SentMessages.Add(secondMsg);
            secondMsg.Recievers.Add(user1);
            user1.Messages.Add(secondMsg);
            secondMsg.ReplyTo = firstMsg;
            firstMsg.Replies.Add(secondMsg);
        }
        
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

        private MessageController CreateMessageControllerAs(string userName, IModel model)
        {
            var mock = new Mock<ControllerContext>();
            mock.SetupGet(p => p.HttpContext.User.Identity.Name).Returns(userName);
            mock.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);

            var controller = new MessageController(model);
            controller.ControllerContext = mock.Object;
            
            return controller;
        }

        /// <summary>
        ///A test for Index
        ///</summary>
        [TestMethod()]
        public void IndexTest()
        {
            MessageController target = CreateMessageControllerAs("hpotter", TestModel);
            ViewResult result = target.Index(1) as ViewResult;
            MessageListView actual = result.ViewData.Model as MessageListView;
            
            Assert.AreEqual(actual.ActualPage, 1);
            Assert.AreEqual(actual.LastPage, 1);

            MessageModelView message = actual.MessageList.ToArray()[0];
            Assert.AreEqual(message.Indent, 0);
            Assert.IsFalse(message.Recieved);
            Assert.AreEqual(message.TextPreview, "Text");

            MessageModelView message2 = actual.MessageList.ToArray()[1];
            Assert.AreEqual(message2.Indent, 20);
            Assert.IsTrue(message2.Recieved);
            Assert.AreEqual(message2.TextPreview, "Another Text");

            result = target.Index(2) as ViewResult;
            Assert.AreEqual(result.ViewName, "NotExist");

            result = target.Index(0) as ViewResult;
            Assert.AreEqual(result.ViewName, "NotExist");

            result = target.Index(-1) as ViewResult;
            Assert.AreEqual(result.ViewName, "NotExist");
            
        }

        /// <summary>
        ///A test for Detail
        ///</summary>
        [TestMethod()]
        public void DetailTest()
        {
            MessageController target = CreateMessageControllerAs("hpotter", TestModel);
            int messageID = 1;
            ViewResult result = target.Detail(messageID) as ViewResult;
            Message actual = result.ViewData.Model as Message;

            Assert.AreEqual(actual, TestModel.MessageList.Single(m => m.Id == messageID));
        }
    }
}
