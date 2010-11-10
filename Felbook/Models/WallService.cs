using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Felbook.Models
{
	public interface IWallService
	{
		void Add(Status status, IEnumerable<User> users);

		IEnumerable<WallItem> GetWall(User user, int limit = 20);

		int GetUnreadCount(User user);

		void MarkAllWallItemsRead(User user);
	}

	public class WallService : IWallService
	{
        #region Proměnné
        private FelBookDBEntities db;
        #endregion

		#region Konstruktor

		public WallService(FelBookDBEntities DBEntities)
		{
			db = DBEntities;
		}

		#endregion

		public void Add(Status status, IEnumerable<User> users)
		{
			foreach (var user in users)
			{
				var wallItem = new WallItem {
					Unread = true,
					Status = status,
					User = user,
				};

				db.WallItemSet.AddObject(wallItem);
			}

			db.SaveChanges();
		}


		public IEnumerable<WallItem> GetWall(User user, int limit = 20)
		{
			return user.WallItems.OrderByDescending(w => w.Id).Take(limit).ToArray();
		}


		public int GetUnreadCount(User user)
		{
			return user.WallItems.Where(w => w.Unread).Count();
		}


		public void MarkAllWallItemsRead(User user)
		{
			foreach (var unreadItem in user.WallItems.Where(w => w.Unread))
			{
				unreadItem.Unread = false;
			}

			db.SaveChanges();
		}
	}
}