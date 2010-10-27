using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Felbook.Models
{
    public class SystemService : ISystemService
    {
        #region Proměnné
        private FelBookDBEntities db = new FelBookDBEntities();
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
                   where users.Name.Contains(str) || users.Surname.Contains(str)
                   select users;
        }

        /// <summary>
        /// Vrátí skupiny ve kterých se v názvech objevuje daný řetězec
        /// </summary>
        /// <param name="str">Řetězec pomocí kterého hledáme</param>
        /// <returns>recordset skupin</returns>
        public IQueryable<Group> SearchGroups(string str)
        {
            return from groups in db.GroupSet
                   where groups.Name.Contains(str) ||
                   groups.Description.Contains(str)
                   select groups;
        }

        #endregion
    
    }
}