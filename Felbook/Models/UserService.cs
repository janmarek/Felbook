using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Felbook.Models
{
    public class UserService : IUserService
    {

        #region Proměnné
        private FelBookDBEntities db;
        #endregion

		#region Konstruktor

		public UserService(FelBookDBEntities DBEntities)
		{
			db = DBEntities;
		}
		
		#endregion

		#region Metody

		/// <summary>
		/// Vrátí uživatelé ve kterých se objevuje daný řetězec
		/// </summary>
		/// <param name="str">Řetězec pomocí kterého hledáme</param>
		/// <returns>recordset uživatelů</returns>
		public IQueryable<User> SearchUsers(string str)
		{
			return from users in db.UserSet
				   where users.Name.Contains(str) || users.Surname.Contains(str) || users.Username.Contains(str)
				   select users;
		}

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
			db.SaveChanges();
        }

        /// <summary>
        /// Smaže uživatele s jeho veškerými údaji v jiných entitách
        /// </summary>
        /// <param name="usr">Daný uživatel ke smazání</param>
        public void Delete(User usr)
        {
            db.DeleteObject(usr);
			db.SaveChanges();
        }

        /// <summary>
        /// Přidá daného uživatele do skupiny, jako parametr je typ členství
        /// </summary>
        /// <param name="usr">Uživatel</param>
        /// <param name="grp">Skupina</param>
        public void JoinGroup(User usr, Group grp)
        {
			usr.JoinedGroups.Add(grp);
            grp.Users.Add(usr);
			db.SaveChanges();
        }

        /// <summary>
        /// Odebere daného uživatele ze skupiny
        /// </summary>
        /// <param name="usr">Uživatel který se bude mazat</param>
        /// <param name="grp">Skupina ze které se bude mazat</param>
        public void LeaveGroup(User usr, Group grp)
        {
			usr.JoinedGroups.Remove(grp);
            grp.Users.Remove(usr);
			db.SaveChanges();
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
        public void FollowUser(User user, User follower)
        {
            user.Followers.Add(follower);
			db.SaveChanges();
        }

        /// <summary>
        /// Najde uživatele podle ID
        /// </summary>
        /// <param name="id">ID uživatele</param>
        /// <returns>Vrátí daného uživatele podle ID</returns>
        public User GetById(int id) {
            return db.UserSet.Single(u => u.Id == id);
        }

        /// <summary>
        /// Najde uživatele podle jména
        /// </summary>
        /// <param name="name">Jméno</param>
        /// <returns>Uživatel</returns>
        public User FindByUsername(string userName) {
            return db.UserSet.Single(u => u.Username == userName);
        }

        /// <summary>
        /// Přidá status k uživateli
        /// </summary>
        /// <param name="user">Uživatel</param>
        /// <param name="st">Status</param>
        public void AddStatus(User user, Status st) {
            User usr = FindByUsername(user.Username);
            usr.Statuses.Add(st);
			db.SaveChanges();
        }


		public void Edit(User user)
		{
			db.SaveChanges();
		}
		#endregion
	}
}