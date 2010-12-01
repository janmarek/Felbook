using Felbook.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using System.Collections.Generic;
using System.Linq;

namespace Felbook.Tests
{
	[TestClass]
	public class WallServiceTest
	{
		#region variables
		private FelBookDBEntities db = new FelBookDBEntities();
		private WallService service;
		private User user1;
		private Status status1;
		#endregion

		#region init & cleanup
		
		[TestInitialize]
		public void Init()
		{
			db.ExecuteStoreCommand("DELETE FROM WallItemSet");

			service = new WallService(db);

			user1 = new User {
				Username = "user" + db.UserSet.Count(),
				Mail = "mail@example.cz",
				Name = "John",
				Surname = "Doe",
			};
			user1.ChangePassword("123456");
			db.UserSet.AddObject(user1);

			status1 = new Status {
				User = user1,
				Text = "Status",
				Created = DateTime.Now,
			};
			db.StatusSet.AddObject(status1);
		}
		
		
		[TestCleanup]
		public void Cleanup()
		{
		}

		#endregion

		[TestMethod]
		public void AddTest()
		{
			service.Add(status1, new User[] { user1 });
			Assert.AreEqual(1, db.WallItemSet.Where(w => w.Unread && w.User.Id == user1.Id && w.Status.Id == status1.Id).Count());
		}

		[TestMethod]
		public void GetUnreadCountTest()
		{
			int count = service.GetUnreadCount(user1);
			Assert.AreEqual(0, count);
			service.Add(status1, new User[] { user1 });
			count = service.GetUnreadCount(user1);
			Assert.AreEqual(1, count);
		}

		[TestMethod]
		public void GetWallTest()
		{
			// přidat statusy
			service.Add(status1, new User[] { user1 });
			var status2 = new Status {
				Text = "Blabla",
				User = user1,
			};
			service.Add(status2, new User[] { user1 });

			// default
			var result = service.GetWall(user1);
			Assert.AreEqual(2, result.Count());
			var item = result.First();
			Assert.AreEqual(user1.Id, item.User.Id);
			Assert.AreEqual(status2.Id, item.Status.Id);

			// omezení počtu výsledků
			Assert.AreEqual(1, service.GetWall(user1, 1).Count());
		}

		[TestMethod]
		public void MarkAllWallItemsReadTest()
		{
			service.Add(status1, new User[] { user1 });
			service.MarkAllWallItemsRead(user1);
			Assert.AreEqual(0, service.GetUnreadCount(user1));
		}
	}
}
