using Felbook.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using Felbook.Models;
using System.Web.Mvc;

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


        /// <summary>
        ///A test for DbEntities
        ///</summary>
        [TestMethod()]
        public void DbEntitiesTest()
        {
            MessageController target = new MessageController();
            FelBookDBEntities expected = new FelBookDBEntities();
            FelBookDBEntities actual;
            target.DbEntities = expected;
            actual = target.DbEntities;
            Assert.AreEqual(expected, actual);
           
        }

        /// <summary>
        ///A test for Index
        ///</summary>
        [TestMethod()]
        public void IndexTest()
        {
            MessageController target = new MessageController();
            string username = " ";
            ViewResult actual = target.Index(username) as ViewResult;
            Assert.IsNotNull(actual);
            // TODO - tenhle test trochu vylepšit
        }
    }
}
