using Felbook.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Data.EntityClient;
using System.Data.Objects;

namespace Felbook.Tests
{
    
	[TestClass]
	public class GroupTest
	{
		// private FelBookDBEntities db = new FelBookDBEntities();

		#region Additional test attributes

		[ClassInitialize()]
		public static void MyClassInitialize(TestContext testContext)
		{
			// TestData.Insert();
		}
		
		
		[ClassCleanup]
		public static void MyClassCleanup()
		{

		}
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
		///A test for HasMember
		///</summary>
		[TestMethod]
		public void HasMemberTest()
		{
			Group group = new Group();
			User user1 = new User();
			User user2 = new User();
			group.Users.Add(user2);

			Assert.IsFalse(group.HasMember(user1));			
			Assert.IsTrue(group.HasMember(user2));
		}

		/// <summary>
		///A test for IsAdminedBy
		///</summary>
		[TestMethod]
		public void IsAdminedByTest()
		{
			Group group = new Group();
			User user = new User();
			Assert.IsFalse(group.IsAdminedBy(user));
			group.Administrators.Add(user);
			Assert.IsTrue(group.IsAdminedBy(user));
		}

		/// <summary>
		///A test for IsCreatedBy
		///</summary>
		[TestMethod]
		public void IsCreatedByTest()
		{
			Group group = new Group();
			User user = new User();
			Assert.IsFalse(group.IsCreatedBy(user));
			group.Creator = user;
			Assert.IsTrue(group.IsCreatedBy(user));
		}
	}
}
