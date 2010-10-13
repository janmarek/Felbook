using System;
using System.Linq;

namespace Felbook.Models
{

    public class GroupRepository : IGroupRepository
    {
        // --- Proměnné ----------------------
        #region Proměnné
        FelbookDataContext db = new FelbookDataContext();
        #endregion


        // --- Metody ----------------------
        #region Metody

        /// <summary>
        /// Vrátí true/false podle toho jeslti je uživatel ve skupině
        /// </summary>
        /// <param name="usr">Uživatel</param>
        /// <param name="grp">Skupina</param>
        /// <returns></returns>
        public bool IsInGroup(User usr, Group grp) {
            var query = from memberships in db.MembershipInGroups
                        where memberships.GroupID == grp.GroupID
                        join i in db.Users
                        on memberships.UserID equals i.UserID
                        select memberships;    
            if(query.Count()>0){
                return true;
            } else {
                return false;
            }
        }

        /// <summary>
        /// Vrátí uživatele které patří dané skupině
        /// </summary>
        /// <param name="grp">Skuina</param>
        /// <returns>recordset uživatelů</returns>
        public IQueryable<User> GetUsers(Group grp)
        {
            return from users in db.Users
                   join i in db.MembershipInGroups
                   on users.UserID equals i.UserID
                   select users;
        }

        /// <summary>
        /// Vrátí informace o dané skupině
        /// </summary>
        /// <param name="grp">Skupina</param>
        /// <returns>Recordset informací</returns>
        public IQueryable<Information> GetInformations(Group grp)
        {
            return from infoGroup in db.InfoAboutGroups
                   where infoGroup.GroupID == grp.GroupID
                   join i in db.Informations
                   on infoGroup.InfoID equals i.InfoID
                   select i;
        }

        /// <summary>
        /// Přidání informace do skupiny
        /// </summary>
        /// <param name="grp">Skupina</param>
        /// <param name="info">Informace</param>
        public void AddInformation(Group grp, Information info)
        {
            db.InfoAboutGroups.InsertOnSubmit(new InfoAboutGroup{GroupID = grp.GroupID, InfoID = info.InfoID});
        }
        
        
        //void DeleteInformation(Information info);

        public void SetInformation(int idInformation, Information newInfo)
        {
            var oneInformation = (from info in db.Informations
                           where info.InfoID == idInformation
                           select info).Single();

            oneInformation.ParentInfoID = newInfo.ParentInfoID;
            oneInformation.Description = newInfo.Description;
            oneInformation.InfoAboutUser = newInfo.InfoAboutUser;
            oneInformation.InfoInMessage = newInfo.InfoInMessage;
            oneInformation.TypeOfInfo = newInfo.TypeOfInfo;
            oneInformation.Content = newInfo.Content;
        }

        /// <summary>
        /// Přidá novou skupinu do skupin
        /// </summary>
        /// <param name="grp">Skupina</param>
        public void Add(Group grp) {
            db.Groups.InsertOnSubmit(grp);
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