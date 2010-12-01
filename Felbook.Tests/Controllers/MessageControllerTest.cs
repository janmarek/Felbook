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
        [TestInitialize()]
        public void MyTestInitialize()
        {

            // Inicicalizace a naplnění modelu pro testy            
            TestModel = new MockModel();

            User user1 = User.CreateUser(0, "Jindra", "Hrnčír", DateTime.Now,
                  "hrncir.jindra@nebelvir.br", "hpotter", "alohomora");
            TestModel.UserList.Add(user1);

            User user2 = User.CreateUser(1, "Tomáš", "Raddle", DateTime.Now,
                "tomas.raddle@zmijozel.br", "voltmetr", "avadaKadevra");
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
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion

        /// <summary>
        /// Pomocná metoda pro vytvoření controlleru s falšeným Contextem
        /// </summary>
        /// <param name="userName">uživatelské jméno "jakoby přihlášeného" uživatele</param>
        /// <param name="model">model, ze kterého bude controller získávat data</param>
        /// <returns>Controller s falšeným Contextem</returns>
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
        /// A test for Index
        /// </summary>
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
        /// A test for Detail
        /// </summary>
        [TestMethod()]
        public void DetailTest()
        {
            string username = "hpotter";
            MessageController targetController = CreateMessageControllerAs(username, TestModel);
            User user = TestModel.UserList.Single(m => m.Username == username);

            // Na odesílané zprávě mě přečtený/nepřečtený nezajímá
            Message targetMessage = TestModel.MessageList.Single(m => m.Id == 0);
            ViewResult result = targetController.Detail(targetMessage.Id) as ViewResult;
            Message actual = result.ViewData.Model as Message;

            Assert.AreEqual(actual, targetMessage);

            // Na přijaté zprávě mě přečtený/nepřečtený zajímá
            targetMessage = TestModel.MessageList.Single(m => m.Id == 1);
            Assert.IsFalse(targetMessage.Readers.Contains(user));

            result = targetController.Detail(targetMessage.Id) as ViewResult;
            actual = result.ViewData.Model as Message;

            Assert.AreEqual(actual, targetMessage);
            Assert.IsTrue(targetMessage.Readers.Contains(user));


            result = targetController.Detail(2) as ViewResult;
            Assert.AreEqual(result.ViewName, "NotAuthorized");

            result = targetController.Detail(-1) as ViewResult;
            Assert.AreEqual(result.ViewName, "NotAuthorized");
        }

        /// <summary>
        /// A test for UnreadMessage
        /// </summary>
        [TestMethod()]
        public void UnreadMessageTest()
        {
            string username = "hpotter";
            MessageController targetController = CreateMessageControllerAs(username, TestModel);
            User user = TestModel.UserList.Single(m => m.Username == username);

            Message targetMessage = TestModel.MessageList.Single(m => m.Id == 1);
            targetMessage.Readers.Add(user);
            user.ReadMessages.Add(targetMessage);

            RedirectToRouteResult result = targetController.UnreadMessage(targetMessage.Id) as RedirectToRouteResult;
            Assert.IsNotNull(result);
            Assert.IsFalse(targetMessage.Readers.Contains(user));
            Assert.IsFalse(user.ReadMessages.Contains(targetMessage));

            result = targetController.UnreadMessage(2) as RedirectToRouteResult;
            Assert.IsNotNull(result);

            result = targetController.UnreadMessage(-1) as RedirectToRouteResult;
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// A test for SendMessage (page)
        /// </summary>
        [TestMethod()]
        public void SendMessagePageTest()
        {
            string username = "hpotter";
            MessageController targetController = CreateMessageControllerAs(username, TestModel);
            
            ViewResult result = targetController.SendMessage() as ViewResult;
            SendMessageModel actual = result.ViewData.Model as SendMessageModel;

            Assert.IsNotNull(actual.AutocompleteGroups);
            Assert.IsNotNull(actual.AutocompleteUsers);
            Assert.IsNull(actual.PrevMessage);
            Assert.IsNull(actual.Text);
        }

        /// <summary>
        /// A test for ReplyMessage
        /// </summary>
        [TestMethod()]
        public void ReplyMessageTest()
        {
            string username = "hpotter";
            MessageController targetController = CreateMessageControllerAs(username, TestModel);
            User user = TestModel.UserList.Single(m => m.Username == username);
            
            ViewResult result = targetController.ReplyMessage(0) as ViewResult;
            Assert.AreEqual(result.ViewName, "NotAuthorized");

            Message message = TestModel.MessageList.Single(m => m.Id == 1);
            result = targetController.ReplyMessage(message.Id) as ViewResult;
            SendMessageModel actual = result.ViewData.Model as SendMessageModel;

            Assert.IsNull(actual.AutocompleteGroups);
            Assert.IsNull(actual.AutocompleteUsers);
            Assert.AreEqual(message, actual.PrevMessage);
            Assert.IsNull(actual.Text);
        }

        /// <summary>
        /// A test for SendMessage (action)
        /// </summary>
        [TestMethod()]
        public void SendMessageActionTest()
        {
            string senderName = "hpotter";
            MessageController targetController = CreateMessageControllerAs(senderName, TestModel);
            User sender = TestModel.UserList.Single(m => m.Username == senderName);

            string recieverName = "voltmetr";
            User reciever = TestModel.UserList.Single(m => m.Username == recieverName);
            string msgText = "Some text";


            SendMessageModel model = new SendMessageModel();
            model.ToUsers = recieverName + "; " + senderName + "; ";
            model.ToGroups = null;
            model.PrevMessageID = -1;
            model.Text = msgText;

            RedirectToRouteResult redirectResult = targetController.SendMessage(model) as RedirectToRouteResult;
            Assert.IsNotNull(redirectResult);
            Assert.AreEqual(3, TestModel.MessageList.Count);

            Message targetMessage = TestModel.MessageList.Last();
            Assert.AreEqual(sender, targetMessage.Sender);
            Assert.IsNull(targetMessage.ReplyTo);
            Assert.AreEqual(msgText, targetMessage.Text);
            Assert.IsTrue(targetMessage.Recievers.Contains(reciever));
            Assert.IsFalse(targetMessage.Recievers.Contains(sender));


            model = new SendMessageModel();
            model.ToUsers = null;
            model.ToGroups = null;
            model.PrevMessageID = -1;
            model.Text = msgText;

            ViewResult viewResult = targetController.SendMessage(model) as ViewResult;
            Assert.IsNotNull(viewResult);
            Assert.AreEqual(3, TestModel.MessageList.Count);

            SendMessageModel actual = viewResult.ViewData.Model as SendMessageModel;
            Assert.AreEqual(msgText, actual.Text);


            model = new SendMessageModel();
            model.ToUsers = recieverName;
            model.ToGroups = null;
            model.PrevMessageID = -1;
            model.Text = "";

            viewResult = targetController.SendMessage(model) as ViewResult;
            Assert.IsNotNull(viewResult);
            Assert.AreEqual(3, TestModel.MessageList.Count);

            actual = viewResult.ViewData.Model as SendMessageModel;
            Assert.AreEqual(recieverName, actual.ToUsers);
        }

    }
}
