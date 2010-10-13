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
        IQueryable<User> GetFriends(User usr); //přátelé uživatele
        IQueryable<User> GetCommonFriends(User usrFirst, User usrSecond); //společní přátelé

        void SendMessage(Message msg, User usr); //poslat zprávu uživately
        void Add(User usr); 
        void Delete(User usr); 
        void Save();
        void JoinToGroup(User usr, Group grp, string TypeOfMem); //přidání do skupiny
        void LeaveGroup(User usr, Group grp); //odchod ze skupiny
        void MakeFriendship(User usrFirst, User usrSecond, string type); //vytvoření přátelství mezi dvěma uživately
        void SetRights(User usr, int parInfoID, Right rights); //nastavení práv na nějakou jedinečnou informaci  
    }
}
