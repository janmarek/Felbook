using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Felbook.Models
{
    public interface IUserService
    {
        User FindById(int id); //najde užiatele podle ID
        User FindByUsername(string name); //najde uživatele podle jména

        bool IsUserInGroup(User usr, Group grp);
        bool IsEmailUnique(string email, User usr);
        void Add(User user);
		void Edit(User user);
        void Delete(User usr);
        void JoinGroup(User usr, Group grp); //přidání do skupiny
        void LeaveGroup(User usr, Group grp); //odchod ze skupiny
        void FollowUser(User user, User follower); //vytvoření něco jako přátelství
    }
}
