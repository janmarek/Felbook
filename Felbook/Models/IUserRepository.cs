using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Felbook.Models
{
    public interface IUserRepository
    {
        IQueryable<Message> GetIncomingMessages(User usr); //zprávy uživatele - příchozí
        IQueryable<Message> GetOutcomingMessages(User usr); //zprávy uživatele - odchozí
        IQueryable<User> GetFollowers(User usr); //přátelé uživatele
        IQueryable<User> GetCommonFollowers(User usrFirst, User usrSecond); //společní followeři

        bool IsUserInGroup(User usr, Group grp);
        void SendMessage(Message msg); //poslat zprávu uživately
        void Add(User usr); 
        void Delete(User usr); 
        void Save();
        void JoinToGroup(User usr, Group grp); //přidání do skupiny
        void LeaveGroup(User usr, Group grp); //odchod ze skupiny
        void FollowUser(User user, User follower); //vytvoření něco jako přátelství
    }
}
