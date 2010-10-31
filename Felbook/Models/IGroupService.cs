using System;
namespace Felbook.Models
{
	public interface IGroupService
	{
		void Add(Group grp);
		void AddSubGroup(Group group, Group child);
		void Delete(Group grp);
		System.Linq.IQueryable<User> GetUsers(Group grp);
		Group FindById(int id);
		System.Linq.IQueryable<Group> SearchGroups(string str);
	}
}
