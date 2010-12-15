using Felbook.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using System.Collections.Generic;
using System.Linq;

namespace Felbook.Tests
{
    
    
    /// <summary>
    ///This is a test class for EventServiceTest and is intended
    ///to contain all EventServiceTest Unit Tests
    ///</summary>
    [TestClass()]
    public class EventServiceTest
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
        ///A test for EventService Constructor
        ///</summary>
        [TestMethod()]
        public void EventServiceConstructorTest()
        {
            FelBookDBEntities DBEntities = new FelBookDBEntities();
            EventService target = new EventService(DBEntities);
            Assert.IsNotNull(target);
        }

        /// <summary>
        ///A test for AddEvent
        ///</summary>
        [TestMethod()]
        public void AddEventTest()
        {
            FelBookDBEntities DBEntities = new FelBookDBEntities();
            EventService target = new EventService(DBEntities);

            User mockUser = User.CreateUser(0, "test", "test",
               DateTime.Now, "mail", "test user", "");
            DBEntities.UserSet.AddObject(mockUser);
            DBEntities.SaveChanges();

            DateTime from = DateTime.Now;
            DateTime to = DateTime.Now;

            string name = "Test event";
            string text = "Description of test event...";

            target.AddEvent(mockUser, null, from, to, name, text);
            Event actual = DBEntities.EventSet.Single(e => e.Name == name);

            Assert.AreEqual(mockUser, actual.User);
            Assert.IsNull(actual.Group);
            Assert.AreEqual(from, actual.From);
            Assert.AreEqual(to, actual.To);
            Assert.AreEqual(name, actual.Name);
            Assert.AreEqual(text, actual.Text);

            DBEntities.EventSet.DeleteObject(actual);
            DBEntities.UserSet.DeleteObject(mockUser);
            DBEntities.SaveChanges();
            
        }

        /// <summary>
        ///A test for DeleteEvent
        ///</summary>
        [TestMethod()]
        public void DeleteEventTest()
        {
            FelBookDBEntities DBEntities = new FelBookDBEntities();
            EventService target = new EventService(DBEntities);

            User mockUser = User.CreateUser(0, "test", "test",
               DateTime.Now, "mail", "test user", "");
            DBEntities.UserSet.AddObject(mockUser);

            DateTime from = DateTime.Now;
            DateTime to = DateTime.Now;
            string name = "Test event";
            string text = "Description of test event...";

            Event actual = Event.CreateEvent(0, from, to, name, text);
            actual.User = mockUser;
            DBEntities.EventSet.AddObject(actual);
            DBEntities.SaveChanges();

            int id = actual.Id; 
            Assert.IsTrue(DBEntities.EventSet.ToList().Contains(actual));

            target.DeleteEvent(id);

            Assert.IsFalse(DBEntities.EventSet.ToList().Contains(actual));

            DBEntities.UserSet.DeleteObject(mockUser);
            DBEntities.SaveChanges();
            
        }

        /// <summary>
        ///A test for FindEventById
        ///</summary>
        [TestMethod()]
        public void FindEventByIdTest()
        {
            FelBookDBEntities DBEntities = new FelBookDBEntities();
            EventService target = new EventService(DBEntities);

            User mockUser = User.CreateUser(0, "test", "test",
               DateTime.Now, "mail", "test user", "");
            DBEntities.UserSet.AddObject(mockUser);

            DateTime from = DateTime.Now;
            DateTime to = DateTime.Now;
            string name = "Test event";
            string text = "Description of test event...";

            Event expected = Event.CreateEvent(0, from, to, name, text);
            expected.User = mockUser;
            DBEntities.EventSet.AddObject(expected);
            DBEntities.SaveChanges();

            int id = expected.Id; 
            
            Event actual = target.FindEventById(id);
            Assert.AreEqual(expected, actual);

            Assert.IsNull(target.FindEventById(-1));
            
            DBEntities.EventSet.DeleteObject(actual);
            DBEntities.UserSet.DeleteObject(mockUser);
            DBEntities.SaveChanges();
            
        }

        /// <summary>
        ///A test for GetEvents
        ///</summary>
        [TestMethod()]
        public void GetEventsTest()
        {
            FelBookDBEntities DBEntities = new FelBookDBEntities();
            EventService target = new EventService(DBEntities);

            User mockUser = User.CreateUser(0, "test", "test",
               DateTime.Now, "mail", "test user", "");
            DBEntities.UserSet.AddObject(mockUser);
            DBEntities.SaveChanges();
            
            IEnumerable<Event> actual = target.GetEvents(mockUser);
            Assert.AreEqual(0, actual.ToList().Count);

            DateTime from = DateTime.Now;
            DateTime to = DateTime.Now;
            string name = "Test event 1";
            string text = "Description of test event...";

            Event firstEvent = Event.CreateEvent(0, from, to, name, text);
            firstEvent.User = mockUser;
            DBEntities.EventSet.AddObject(firstEvent);
            DBEntities.SaveChanges();

            actual = target.GetEvents(mockUser);
            Assert.AreEqual(1, actual.ToList().Count);
            Assert.IsTrue(actual.ToList().Contains(firstEvent));

            Event secondEvent = Event.CreateEvent(0, from, to, name, text);
            secondEvent.User = mockUser;
            DBEntities.EventSet.AddObject(secondEvent);
            DBEntities.SaveChanges();

            actual = target.GetEvents(mockUser);
            Assert.AreEqual(2, actual.ToList().Count);
            Assert.IsTrue(actual.ToList().Contains(firstEvent));
            Assert.IsTrue(actual.ToList().Contains(secondEvent));

            DBEntities.EventSet.DeleteObject(secondEvent);
            DBEntities.EventSet.DeleteObject(firstEvent);
            DBEntities.UserSet.DeleteObject(mockUser);
            DBEntities.SaveChanges();
            
        }
    }
}
