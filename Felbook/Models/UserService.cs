using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Felbook.Models
{
    public class UserService : IUserService
    {

        #region Proměnné
        private FelBookDBEntities db = new FelBookDBEntities();
        #endregion

        #region Metody

        /// <summary>
        /// Vrátí true/false podle toho jeslti je uživatel ve skupině
        /// </summary>
        /// <param name="usr">Uživatel</param>
        /// <param name="grp">Skupina</param>
        /// <returns>true nebo false podle toho jestli v té skupině je nebo není</returns>
        public bool IsUserInGroup(User usr, Group grp)
        {
            return grp.Users.Contains(usr);
        }

        /// <summary>
        /// Vrátí veškeré zprávy které jsou odchozí danému uživateli
        /// </summary>
        /// <param name="usr">Uživatel který odeslal zprávy</param>
        /// <returns>Vrací recordset těch odchozích zpráv</returns>
        public IQueryable<Message> GetOutcomingMessages(User usr)
        {
            return usr.SentMessages.AsQueryable();
        }

        /// <summary>
        /// Vrátí veškeré zprávy které jsou příchozí danému uživateli
        /// </summary>
        /// <param name="usr">Uživatel který přijal zprávy</param>
        /// <returns>Vrácí řádky kde každý řádek je Message</returns>
        public IQueryable<Message> GetIncomingMessages(User usr)
        {
            return from recMessages in db.MessageSet
                   where recMessages.Users.Contains(usr)
                   select recMessages;
        }
        
        /// <summary>
        /// Odeslání zprávy uživately/uživatelům
        /// </summary>
        /// <param name="msg">Zpráva která bude poslána</param>
        public void SendMessage(Message msg)
        {
            db.AddToMessageSet(msg);
        }

        /// <summary>
        /// Vrátí společné přátelé dvou uživatelů
        /// </summary>
        /// <param name="usrFirst">První uživatel</param>
        /// <param name="usrSecond">Další uživatel který má společné přátelé s tím prvním</param>
        /// <returns>Množina společných uživatelů</returns>
        public IQueryable<User> GetCommonFollowers(User usrFirst, User usrSecond)
        {
            // Intersect je průnik dvou množin
            return usrFirst.Followers.Intersect(usrSecond.Followers).AsQueryable();
        }

        /// <summary>
        /// Vytvoří se nový uživatel
        /// </summary>
        /// <param name="usr">Daný uživatel který se vytvoří</param>
        public void Add(User usr)
        {
            db.AddToUserSet(usr);
        }

        /// <summary>
        /// Smaže uživatele s jeho veškerými údaji v jiných entitách
        /// </summary>
        /// <param name="usr">Daný uživatel ke smazání</param>
        public void Delete(User usr)
        {
            db.DeleteObject(usr);
        }

        /// <summary>
        /// Přidá daného uživatele do skupiny, jako parametr je typ členství
        /// </summary>
        /// <param name="usr">Uživatel</param>
        /// <param name="grp">Skupina</param>
        public void JoinToGroup(User usr, Group grp)
        {
            grp.Users.Add(usr);
        }

        /// <summary>
        /// Odebere daného uživatele ze skupiny
        /// </summary>
        /// <param name="usr">Uživatel který se bude mazat</param>
        /// <param name="grp">Skupina ze které se bude mazat</param>
        public void LeaveGroup(User usr, Group grp)
        {
            grp.Users.Remove(usr);
        }


        /// <summary>
        /// Získá recordset přátel daného usera
        /// </summary>
        /// <param name="usr">Uživatel</param>
        /// <returns></returns>
        public IQueryable<User> GetFollowers(User usr)
        {
            return usr.Followers.AsQueryable();
        }

        /// <summary>
        /// Vytvoří přátelství mezi dvěma uživately
        /// </summary>
        /// <param name="usrFirst">První uživatel</param>
        /// <param name="usrSecond">Druhý uživatel</param>
        /// <param name="type">Typ přátelství - zatím jako string</param>
        public void FollowUser(User user, User follower)
        {
            user.Followers.Add(follower);
        }

        /// <summary>
        /// Najde uživatele podle ID
        /// </summary>
        /// <param name="id">ID uživatele</param>
        /// <returns>Vrátí daného uživatele podle ID</returns>
        public User SearchById(int id) {
            return db.UserSet.Single(u => u.Id == id);
        }

        /// <summary>
        /// Najde uživatele podle jména
        /// </summary>
        /// <param name="name">Jméno</param>
        /// <returns>Uživatel</returns>
        public User SearchByUserName(string userName) {
            return db.UserSet.Single(u => u.Username == userName);
        }

        /// <summary>
        /// Přidá status k uživateli
        /// </summary>
        /// <param name="user">Uživatel</param>
        /// <param name="st">Status</param>
        public void AddStatus(User user, Status st) {
            User usr = SearchByUserName(user.Username);
            usr.Statuses.Add(st);
        }

        /// <summary>
        /// Uloží se změny do DB - prostě commit
        /// </summary>
        public void Save()
        {
            db.SaveChanges();
        }
        #endregion
    }
}