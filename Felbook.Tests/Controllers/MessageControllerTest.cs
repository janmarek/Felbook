// license MIT

using Felbook.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using Felbook.Models;
using Felbook.Tests.Fakes;
using System.Web.Mvc;
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

        MessageController CreateMessageControllerAs(string userName)
        {
            var mock = new Mock<ControllerContext>();
            mock.SetupGet(p => p.HttpContext.User.Identity.Name).Returns(userName);
            mock.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);

            var controller = new MessageController(new MockModel());
            controller.ControllerContext = mock.Object;
            
            return controller;
        }

        /// <summary>
        ///A test for Index
        ///</summary>
        [TestMethod()]
        public void IndexTest()
        {
            MessageController target = CreateMessageControllerAs("novakjakub");
            ViewResult result = target.Index(1) as ViewResult;
            MessageListView actual = result.ViewData.Model as MessageListView;
            Assert.IsNotNull(actual);

            Assert.AreEqual(actual.ActualPage, 1);
            Assert.AreEqual(actual.LastPage, 1);

            MessageModelView message = actual.MessageList.ToArray()[0];
            Assert.AreEqual(message.Indent, 0);
            Assert.IsFalse(message.Recieved);
            Assert.AreEqual(message.TextPreview, "Text");



            // TODO - tenhle test trochu vylepšit
        }
    }
}
