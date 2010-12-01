using Felbook.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using System.Linq;
using System.Collections.Generic;

namespace Felbook.Tests
{
    
    
    /// <summary>
    ///This is a test class for GroupServiceTest and is intended
    ///to contain all GroupServiceTest Unit Tests
    ///</summary>
    [TestClass()]
    public class GroupServiceTest
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
        ///A test for GroupService Constructor
        ///</summary>
        [TestMethod()]
        public void GroupServiceConstructorTest()
        {
            FelBookDBEntities DBEntities = new FelBookDBEntities();
            IWallService wallService = new WallService(DBEntities);
            GroupService target = new GroupService(DBEntities, wallService);
            Assert.IsNotNull(target);
        }

        /// <summary>
        ///A test for Add
        ///</summary>
        [TestMethod()]
        public void AddTest()
        {
            FelBookDBEntities DBEntities = new FelBookDBEntities();
            IWallService wallService = null; // Snad ji zatím nepotřebuji
            GroupService target = new GroupService(DBEntities, wallService);
 
            User user = User.CreateUser(0, "group", "creator", DateTime.Now,
                "mail", "groupCreator", "1234");
            DBEntities.UserSet.AddObject(user);
            DBEntities.SaveChanges();
            
            Group group = Group.CreateGroup(0, "newGroup", "groupDescription");
            target.Add(user, group);

            Assert.IsTrue(DBEntities.GroupSet.ToList().Contains(group));
            Assert.AreEqual(user, group.Creator);
            Assert.IsTrue(group.Administrators.Contains(user));
            Assert.IsTrue(group.Users.Contains(user));

            Assert.IsTrue(user.AdminedGroups.Contains(group));
            Assert.IsTrue(user.CreatedGroups.Contains(group));
            Assert.IsTrue(user.JoinedGroups.Contains(group));

            DBEntities.GroupSet.DeleteObject(group);
            DBEntities.UserSet.DeleteObject(user);
            DBEntities.SaveChanges();
                        
        }

        /// <summary>
        ///A test for AddSubGroup
        ///</summary>
        [TestMethod()]
        public void AddSubGroupTest()
        {
            FelBookDBEntities DBEntities = new FelBookDBEntities();
            IWallService wallService = null;
            GroupService target = new GroupService(DBEntities, wallService);

            User user = User.CreateUser(0, "group", "creator", DateTime.Now,
                "mail", "groupCreator", "1234");
            DBEntities.UserSet.AddObject(user);

            Group parentGroup = Group.CreateGroup(0, "parentGroup", "parentGroupDescription");
            user.CreatedGroups.Add(parentGroup);
            user.AdminedGroups.Add(parentGroup);
            user.JoinedGroups.Add(parentGroup);
            DBEntities.GroupSet.AddObject(parentGroup);
            DBEntities.SaveChanges();

            Group childGroup = Group.CreateGroup(1, "childGroup", "childGroupDescription");
            target.AddSubGroup(user, parentGroup, childGroup);

            Assert.AreEqual(user, childGroup.Creator);
            Assert.IsTrue(childGroup.Administrators.Contains(user));
            Assert.IsTrue(childGroup.Users.Contains(user));

            Assert.IsTrue(user.AdminedGroups.Contains(childGroup));
            Assert.IsTrue(user.CreatedGroups.Contains(childGroup));
            Assert.IsTrue(user.JoinedGroups.Contains(childGroup));

            Assert.AreEqual(parentGroup, childGroup.Parent);
            Assert.IsTrue(parentGroup.Children.Contains(childGroup));

            DBEntities.GroupSet.DeleteObject(childGroup);
            DBEntities.GroupSet.DeleteObject(parentGroup);
            DBEntities.UserSet.DeleteObject(user);
            DBEntities.SaveChanges();

        }

        /// <summary>
        ///A test for Edit
        ///</summary>
        [TestMethod()]
        public void EditTest()
        {
            FelBookDBEntities DBEntities = new FelBookDBEntities();
            IWallService wallService = null;
            GroupService target = new GroupService(DBEntities, wallService);

            User creator = User.CreateUser(0, "group", "creator", DateTime.Now,
                "mail", "groupCreator", "1234");
            DBEntities.UserSet.AddObject(creator);
            User member = User.CreateUser(0, "group", "member", DateTime.Now,
                "mail", "groupMember", "1234");
            DBEntities.UserSet.AddObject(member);

            Group group = Group.CreateGroup(0, "newGroup", "groupDescription");
            creator.CreatedGroups.Add(group);
            creator.AdminedGroups.Add(group);
            creator.JoinedGroups.Add(group);
            DBEntities.GroupSet.AddObject(group);
            DBEntities.SaveChanges();

            Assert.IsFalse(member.JoinedGroups.Contains(group));
            group.Users.Add(member);

            target.Edit(group);

            Assert.IsTrue(member.JoinedGroups.Contains(group));

            DBEntities.GroupSet.DeleteObject(group);
            DBEntities.UserSet.DeleteObject(member);
            DBEntities.UserSet.DeleteObject(creator);
            DBEntities.SaveChanges();

        }

        /// <summary>
        ///A test for FindById
        ///</summary>
        [TestMethod()]
        public void FindByIdTest()
        {
            FelBookDBEntities DBEntities = new FelBookDBEntities();
            IWallService wallService = null;
            GroupService target = new GroupService(DBEntities, wallService);

            Group actual;
            try
            {
                actual = target.FindById(-1);
                Assert.Fail();
            }
            catch (InvalidOperationException)
            { }

            User user = User.CreateUser(0, "group", "creator", DateTime.Now,
                "mail", "groupCreator", "1234");
            DBEntities.UserSet.AddObject(user);

            Group expected = Group.CreateGroup(0, "newGroup", "groupDescription");
            user.CreatedGroups.Add(expected);
            user.AdminedGroups.Add(expected);
            user.JoinedGroups.Add(expected);
            DBEntities.GroupSet.AddObject(expected);
            DBEntities.SaveChanges();

            actual = target.FindById(expected.Id);
            Assert.AreEqual(expected, actual);

            DBEntities.GroupSet.DeleteObject(expected);
            DBEntities.UserSet.DeleteObject(user);
            DBEntities.SaveChanges();

        }

        /// <summary>
        ///A test for FindByName
        ///</summary>
        [TestMethod()]
        public void FindByNameTest()
        {
            FelBookDBEntities DBEntities = new FelBookDBEntities();
            IWallService wallService = null;
            GroupService target = new GroupService(DBEntities, wallService);

            Group actual;
            try
            {
                actual = target.FindByName(null);
                Assert.Fail();
            }
            catch (InvalidOperationException)
            { }

            User user = User.CreateUser(0, "group", "creator", DateTime.Now,
                "mail", "groupCreator", "1234");
            DBEntities.UserSet.AddObject(user);

            Group expected = Group.CreateGroup(0, "newGroup", "groupDescription");
            user.CreatedGroups.Add(expected);
            user.AdminedGroups.Add(expected);
            user.JoinedGroups.Add(expected);
            DBEntities.GroupSet.AddObject(expected);
            DBEntities.SaveChanges();

            actual = target.FindByName(expected.Name);
            Assert.AreEqual(expected, actual);

            DBEntities.GroupSet.DeleteObject(expected);
            DBEntities.UserSet.DeleteObject(user);
            DBEntities.SaveChanges();
            
        }

        /// <summary>
        ///A test for GetUsers
        ///</summary>
        [TestMethod()]
        public void GetUsersTest()
        {
            FelBookDBEntities DBEntities = new FelBookDBEntities();
            IWallService wallService = null;
            GroupService target = new GroupService(DBEntities, wallService);

            User creator = User.CreateUser(0, "group", "creator", DateTime.Now,
                "mail", "groupCreator", "1234");
            DBEntities.UserSet.AddObject(creator);
            User member = User.CreateUser(0, "group", "member", DateTime.Now,
                "mail", "groupMember", "1234");
            DBEntities.UserSet.AddObject(member);

            Group group = Group.CreateGroup(0, "newGroup", "groupDescription");
            creator.CreatedGroups.Add(group);
            creator.AdminedGroups.Add(group);
            creator.JoinedGroups.Add(group);
            DBEntities.GroupSet.AddObject(group);
            DBEntities.SaveChanges();

            List<User> actual = target.GetUsers(group).ToList();
            Assert.AreEqual(1, actual.Count);
            Assert.IsTrue(actual.Contains(creator));
            Assert.IsFalse(actual.Contains(member));

            group.Users.Add(member);
            DBEntities.SaveChanges();

            actual = target.GetUsers(group).ToList();
            Assert.AreEqual(2, actual.Count);
            Assert.IsTrue(actual.Contains(creator));
            Assert.IsTrue(actual.Contains(member));

            group.Users.Remove(member);
            group.Users.Remove(creator);
            DBEntities.SaveChanges();

            actual = target.GetUsers(group).ToList();
            Assert.AreEqual(0, actual.Count);
            Assert.IsFalse(actual.Contains(creator));
            Assert.IsFalse(actual.Contains(member));

            DBEntities.GroupSet.DeleteObject(group);
            DBEntities.UserSet.DeleteObject(member);
            DBEntities.UserSet.DeleteObject(creator);
            DBEntities.SaveChanges();
                        
        }

        /// <summary>
        ///A test for SearchGroups
        ///</summary>
        [TestMethod()]
        public void SearchGroupsTest()
        {
            FelBookDBEntities DBEntities = new FelBookDBEntities();
            IWallService wallService = null;
            GroupService target = new GroupService(DBEntities, wallService);

            User user = User.CreateUser(0, "group", "creator", DateTime.Now,
                "mail", "groupCreator", "1234");
            DBEntities.UserSet.AddObject(user);

            Group group1 = Group.CreateGroup(0, "foo", "description");
            user.CreatedGroups.Add(group1);
            user.AdminedGroups.Add(group1);
            user.JoinedGroups.Add(group1);
            DBEntities.GroupSet.AddObject(group1);

            Group group2 = Group.CreateGroup(0, "foo2", "description");
            user.CreatedGroups.Add(group2);
            user.AdminedGroups.Add(group2);
            user.JoinedGroups.Add(group2);
            DBEntities.GroupSet.AddObject(group2);

            Group group3 = Group.CreateGroup(0, "moo", "description");
            user.CreatedGroups.Add(group3);
            user.AdminedGroups.Add(group3);
            user.JoinedGroups.Add(group3);
            DBEntities.GroupSet.AddObject(group3);

            DBEntities.SaveChanges();
            
            List<Group> actual = target.SearchGroups("2").ToList();
            Assert.IsFalse(actual.Contains(group1));
            Assert.IsTrue(actual.Contains(group2));
            Assert.IsFalse(actual.Contains(group3));

            actual = target.SearchGroups("foo").ToList();
            Assert.IsTrue(actual.Contains(group1));
            Assert.IsTrue(actual.Contains(group2));
            Assert.IsFalse(actual.Contains(group3));

            actual = target.SearchGroups("moo").ToList();
            Assert.IsFalse(actual.Contains(group1));
            Assert.IsFalse(actual.Contains(group2));
            Assert.IsTrue(actual.Contains(group3));

            actual = target.SearchGroups("abcdef").ToList();
            Assert.IsFalse(actual.Contains(group1));
            Assert.IsFalse(actual.Contains(group2));
            Assert.IsFalse(actual.Contains(group3));

            actual = target.SearchGroups("oo").ToList();
            Assert.IsTrue(actual.Contains(group1));
            Assert.IsTrue(actual.Contains(group2));
            Assert.IsTrue(actual.Contains(group3));

            DBEntities.GroupSet.DeleteObject(group1);
            DBEntities.GroupSet.DeleteObject(group2);
            DBEntities.GroupSet.DeleteObject(group3);
            DBEntities.UserSet.DeleteObject(user);
            DBEntities.SaveChanges();
            
        }
    }
}
