using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Felbook.Models
{
	[MetadataType(typeof(GroupMetadata))]
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


		public IEnumerable<Group> GetAllSubGroups()
		{
			var list = new List<Group>();
			AddChildrenToList(this, list);

			return list;
		}

		private void AddChildrenToList(Group group, List<Group> list)
		{
			foreach (var child in group.Children)
			{
				list.Add(child);
				AddChildrenToList(child, list);
			}
		}


		/// <summary>
		/// Má alespoň jednoho rodiče
		/// </summary>
		/// <returns></returns>
		public bool HasParent()
		{
			return Parent != null;
		}


		/// <summary>
		/// Má člena
		/// </summary>
		/// <param name="user">testovaný uživatel</param>
		/// <returns></returns>
		public bool HasMember(User user)
		{
			return Users.Contains(user);
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

	public class GroupMetadata
	{
		[Required(ErrorMessage="Group name is required.")]
		[DisplayName("Name")]
		public string Name { set; get; }

		[Required(ErrorMessage="Description is required.")]
		[DisplayName("Description")]
		public string Description { set; get; }
	}
}