using System;
using System.Linq;

namespace Felbook.Models
{

    public class UserRepository : IUserRepository
    {
        // --- Proměnné ----------------------
        #region Proměnné
        FelbookDataContext db = new FelbookDataContext();
        #endregion


        // --- Metody ----------------------
        #region Metody

        /// <summary>
        /// Vrátí veškeré zprávy které jsou odchozí danému uživateli
        /// </summary>
        /// <param name="usr">Uživatel který odeslal zprávy</param>
        /// <returns>Vrací recordset těch odchozích zpráv</returns>
        public IQueryable<Message> GetOutcomingMessages(User usr)
        {
            return from messages in db.Messages
                   where messages.FromUser == usr.UserID
                   select messages;
        }
        
        /// <summary>
        /// Vrátí veškeré zprávy které jsou příchozí danému uživateli
        /// </summary>
        /// <param name="usr">Uživatel který přijal zprávy</param>
        /// <returns>Vrácí řádky kde každý řádek je Message</returns>
        public IQueryable<Message> GetIncomingMessages(User usr)
        {
            return from recMessages in db.RecieverLists
                   where recMessages.UserID == usr.UserID
                   join i in db.Messages 
                   on recMessages.MessageID equals i.MessageID
                   select i;
        }
        
        /// <summary>
        /// Odeslání zprávy
        /// </summary>
        /// <param name="msg">Ta zpráva která se vůbec posílá</param>
        /// <param name="usr">Uživatel kterému se zpráva posílá</param>
        public void SendMessage(Message msg, User usr)
        {
            db.Messages.InsertOnSubmit(msg);
            db.RecieverLists.InsertOnSubmit(
            new RecieverList //uloží se nový anonymní ReceiverList
            { //parametry má předáné z msg a destUserId
                MessageID = msg.MessageID,
                UserID = usr.UserID
            });
        }

        /// <summary>
        /// Vrátí společné přátelé dvou uživatelů
        /// </summary>
        /// <param name="usrFirst">První uživatel</param>
        /// <param name="usrSecond">Další uživatel který má společné přátelé s tím prvním</param>
        /// <returns>Množina společných uživatelů</returns>
        public IQueryable<User> GetCommonFriends(User usrFirst, User usrSecond)
        {
            //Poznámka: tady si taky nejsem úplně jistej jestli to půjde, bude to potřeba otestovat
            return from users in db.Users
                   from friendship in db.Friendships
                   where (users.UserID == friendship.UserWhoHas && friendship.WithWhichUser == usrSecond.UserID) ||
                   (users.UserID == usrSecond.UserID && friendship.UserWhoHas == users.UserID && friendship.WithWhichUser == usrFirst.UserID)
                   select users;
        }

        /// <summary>
        /// Vytvoří se nový uživatel
        /// </summary>
        /// <param name="usr">Daný uživatel který se vytvoří</param>
        public void Add(User usr)
        {
            db.Users.InsertOnSubmit(usr);
        }
        
        /// <summary>
        /// Smaže uživatele s jeho veškerými údaji v jiných entitách
        /// </summary>
        /// <param name="usr">Daný uživatel ke smazání</param>
        public void Delete(User usr)
        {
            db.RecieverLists.DeleteAllOnSubmit(usr.RecieverLists); //smaže veškeré zprávy uživatele
            db.Rights.DeleteAllOnSubmit(usr.Rights); //smaže veškerá práva týkající se uživatele
            db.Friendships.DeleteAllOnSubmit(usr.Friendships); //smaže veškerá přátelství s uživately
            db.MembershipInGroups.DeleteAllOnSubmit(usr.MembershipInGroups); //smaže uživatelovy členství ve skupinách
        }

        /// <summary>
        /// Přidá daného uživatele do skupiny, jako parametr je typ členství
        /// </summary>
        /// <param name="usr">Uživatel</param>
        /// <param name="grp">Skupina</param>
        /// <param name="TypeOfMem">Typ členství - zatím jako string možná později vymyslíme něco jiného</param>
        public void JoinToGroup(User usr, Group grp, string TypeOfMem)
        {
            //Pro přehlednost udělaná proměnná jako jeden záznam v MembershipInGroup
            MembershipInGroup memInGrp = new MembershipInGroup{
                GroupID = grp.GroupID,
                TypeOfMembership = TypeOfMem,
                UserID = usr.UserID
            };
            //nyní se ta proměnná uloží do entity
            db.MembershipInGroups.InsertOnSubmit(memInGrp);
        }

        /// <summary>
        /// Odebere daného uživatele ze skupiny
        /// </summary>
        /// <param name="usr">Uživatel</param>
        /// <param name="grp">Skupina</param>
        public void LeaveGroup(User usr, Group grp)
        {
            db.MembershipInGroups.DeleteAllOnSubmit(from membership in db.MembershipInGroups
                                                 where (membership.GroupID == grp.GroupID)
                                                 && (membership.UserID == usr.UserID)
                                                 select membership);
        }
        
        /// <summary>
        /// Nastaví práva na informace o nějaké skupině
        /// </summary>
        /// <param name="usr">Uživatel</param>
        /// <param name="parInfoID">ID infa o skupině</param>
        /// <param name="rights">Práva, tam u kterých stačí předat parametry Modify, Read, Delete</param>
        public void SetRights(User usr, int parInfoID, Right rights){
            Right newRight = new Right{
                UserID = usr.UserID,
                InfoID = parInfoID,
                CanModify = rights.CanModify,
                CanRead = rights.CanDelete,
                CanDelete = rights.CanDelete
            };
            db.Rights.InsertOnSubmit(newRight);
        }

        /// <summary>
        /// Získá recordset přátel daného usera
        /// </summary>
        /// <param name="usr">Uživatel</param>
        /// <returns></returns>
        public IQueryable<User> GetFriends(User usr)
        { 
            return from users in db.Users
                   join i in db.Friendships
                   on users.UserID equals i.UserWhoHas
                   select users;
        }

        /// <summary>
        /// Vytvoří přátelství mezi dvěma uživately
        /// </summary>
        /// <param name="usrFirst">První uživatel</param>
        /// <param name="usrSecond">Druhý uživatel</param>
        /// <param name="type">Typ přátelství - zatím jako string</param>
        public void MakeFriendship(User usrFirst, User usrSecond, string type)
        {
            db.Friendships.InsertOnSubmit(new Friendship{ 
                UserWhoHas = usrFirst.UserID, 
                WithWhichUser = usrSecond.UserID, 
                TypeOfFriendship= type
            });
        }

        /// <summary>
        /// Potvrdí se změny do DB - prostě commit
        /// </summary>
        public void Save()
        {
            db.SubmitChanges();
        }
        #endregion

    }
}