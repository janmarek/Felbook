using System;
namespace Felbook.Models
{
	public interface IGroupService
	{
		void Add(User user, Group grp);
		void AddSubGroup(User user, Group group, Group child);
		//void Delete(Group grp);
		System.Linq.IQueryable<User> GetUsers(Group grp);
		Group FindById(int id);
        Group FindByName(string name);
		System.Linq.IQueryable<Group> SearchGroups(string str);
		void Edit(Group group);
	}
}
