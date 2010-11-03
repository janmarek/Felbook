using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Felbook.Models
{
    public interface IUserService
    {
        IQueryable<Message> GetIncomingMessages(User usr); //zprávy uživatele - příchozí
        IQueryable<Message> GetOutcomingMessages(User usr); //zprávy uživatele - odchozí
        IQueryable<User> GetFollowers(User usr); //přátelé uživatele
        IQueryable<User> GetCommonFollowers(User usrFirst, User usrSecond); //společní followeři

        User GetById(int id); //najde užiatele podle ID
        User FindByUsername(string name); //najde uživatele podle jména

        bool IsUserInGroup(User usr, Group grp);
        void Add(User user);
		void Edit(User user);
        void Delete(User usr);
        void JoinGroup(User usr, Group grp); //přidání do skupiny
        void LeaveGroup(User usr, Group grp); //odchod ze skupiny
        void FollowUser(User user, User follower); //vytvoření něco jako přátelství
        void AddStatus(User user, Status st); //přidá status k uživateli
    }
}
