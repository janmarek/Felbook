using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Felbook.Models
{
	public partial class Group
	{
		/// <summary>
		/// Získání všech rodičů
		/// </summary>
		public List<Group> Parents
		{
			get
			{
				var list = new List<Group>();
				var currentGroup = this;

				while (currentGroup.Parent != null)
				{
					currentGroup = currentGroup.Parent;
					list.Add(currentGroup);
				}

				return list;
			}
		}


		/// <summary>
		/// Má člena
		/// </summary>
		/// <param name="user">testovaný uživatel</param>
		/// <returns></returns>
		public bool HasMember(User user)
		{
			return user.JoinedGroups.Contains(this);
			//return Users.Contains(user);
		}


		/// <summary>
		/// Je skupina administrovaná členem?
		/// </summary>
		/// <param name="user">testovaný uživatel</param>
		/// <returns></returns>
		public bool IsAdminedBy(User user)
		{
			return Administrators.Contains(user);
		}


		/// <summary>
		/// Je skupina vytvořena uživatelem?
		/// </summary>
		/// <param name="user">Testovaný uživatel</param>
		/// <returns></returns>
		public bool IsCreatedBy(User user)
		{
			return Creator == user;
		}

	}
}