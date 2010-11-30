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
		private IWallService wallService;
        #endregion

		#region Konstruktor

		public UserService(FelBookDBEntities DBEntities, IWallService wallService)
		{
			db = DBEntities;
			this.wallService = wallService;
		}
		
		#endregion

		#region Metody

        /// <summary>
        /// Ověří jestli je daný email již v databázi
        /// </summary>
        /// <param name="email">String řetězec email</param>
        /// <param name="usr">Uživatel ke kterému se daný email kontroluje</param>
        /// <returns>vrátí true pokud je unikátní a false pokud již existuje</returns>
        public bool IsEmailUnique(string email, User usr) 
        {
            var result = db.UserSet.Single(u => u.Mail == email);
            if (result != null)
            {
                if (result.Username.Equals(usr.Username)) {
                    return true;
                }
                return false;
            }
            else 
            {
                return true;
            }       
        }

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
		/// První uživatel začne být sledován druhým
		/// </summary>
		/// <param name="usrFirst">První uživatel</param>
		/// <param name="usrSecond">Druhý uživatel</param>
		public void FollowUser(User user, User follower)
		{
			if (user.Followers.Contains(follower))
			{
				throw new UserException("User is already followed");
			}

			user.Followers.Add(follower);
			db.SaveChanges();
		}

		/// <summary>
		/// První uživatel přestane být sledován prvním
		/// </summary>
		/// <param name="usrFirst">První uživatel</param>
		/// <param name="usrSecond">Druhý uživatel</param>
		public void UnfollowUser(User user, User follower)
		{
			if (!user.Followers.Contains(follower))
			{
				throw new UserException("User is not followed by " + user.FullName);
			}

			user.Followers.Remove(follower);
			db.SaveChanges();
		}

        /// <summary>
        /// Najde uživatele podle ID
        /// </summary>
        /// <param name="id">ID uživatele</param>
        /// <returns>Vrátí daného uživatele podle ID</returns>
        public User FindById(int id) {
            return db.UserSet.Single(u => u.Id == id);
        }

        /// <summary>
        /// Najde uživatele podle jména
        /// </summary>
        /// <param name="name">Jméno</param>
        /// <returns>Uživatel</returns>
        public User FindByUsername(string userName) {
            try
            {
                return db.UserSet.Single(u => u.Username == userName);
            }
            catch (Exception)
            {

                return null;
            }     
        }


		public void Edit(User user)
		{
			db.SaveChanges();
		}
		#endregion
	}
}