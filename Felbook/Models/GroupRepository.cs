using System;
using System.Linq;
using System.Collections.Generic;

namespace Felbook.Models
{

    public class GroupRepository : IGroupRepository
    {
        // --- Proměnné ----------------------
        #region Proměnné
        private FelBookDBEntities db = new FelBookDBEntities();
        #endregion

        // --- Metody ----------------------
        #region Metody
        
        /// <summary>
        /// Vrátí uživatelé dané skupiny
        /// </summary>
        /// <param name="grp">Skupina</param>
        /// <returns>Uživatelé skupiny</returns>
        public IQueryable<User> GetUsers(Group grp)
        {
            return grp.Users.AsQueryable();
        }

        /// <summary>
        /// Přidání nové skupiny
        /// </summary>
        /// <param name="grp">skupina</param>
        public void Add(Group grp) {
            db.AddToGroupSet(grp);
        }

        //dodělám až se opraví model je tam chyba u Group
        /*void AddSubGroup(Group parent, Group child) {
            parent.Children = child;
            child.Parent = parent;
            db.AddToGroupSet(child);
        }*/
        
        /// <summary>
        /// Vymazání skupiny, ještě nefunguje zcela správně
        /// </summary>
        /// <param name="grp">Skupina</param>
        public void Delete(Group grp) {
            db.DeleteObject(grp);
        }

        Group SearchById(int id);

        /// <summary>
        /// Potvrdí se změny do DB - prostě commit
        /// </summary>
        public void Save()
        {
            db.SaveChanges();
        }
        #endregion
    }
}