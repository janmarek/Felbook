using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Felbook.Models
{
    public class GroupService : IGroupService
    {
        #region Proměnné
        private FelBookDBEntities db;
		private IWallService wallService;
        #endregion

		#region Konstruktor

		public GroupService(FelBookDBEntities DBEntities, IWallService wallService)
		{
			db = DBEntities;
			this.wallService = wallService;
		}

		#endregion

		#region Metody

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
        /// Přidání nové podgrupy k dané nadgrupě
        /// </summary>
		/// <param name="user">Zakladatel skupiny</param>
		/// <param name="group">ID groupy do které budu přidávat podgrupu</param>
        /// <param name="child">nová podgrupa</param>
        public void AddSubGroup(User user, Group group, Group child) {
			child.Parent = group;
			group.Children.Add(child);
			Add(user, child);
        }

        /// <summary>
        /// Přidání nové skupiny
        /// </summary>
        /// <param name="group">skupina</param>
		/// <param name="user">uživatel, který vytváří skupinu</param>
        public void Add(User user, Group group)
        {
			user.CreatedGroups.Add(group);
			user.AdminedGroups.Add(group);
			user.JoinedGroups.Add(group);

			group.Administrators.Add(user);
			group.Creator = user;
			group.Users.Add(user);

            db.AddToGroupSet(group);
			db.SaveChanges();
        }

        ///// <summary>
        ///// Vymazání skupiny, ještě nefunguje zcela správně
        ///// </summary>
        ///// <param name="grp">Skupina</param>
        //public void Delete(Group grp)
        //{
        //    List<Group> groupForDel = new List<Group>();
        //    Queue<Group> Q = new Queue<Group>();
        //    Group g;
        //    Q.Enqueue(grp);
        //    do
        //    {
        //        g = Q.Dequeue();
        //        groupForDel.Add(g);
        //        for (int i = 0; i < g.Children.Count; i++)
        //        {
        //            Q.Enqueue(g.Children.ElementAt(i));
        //        }
        //    } while (Q.Count > 0); //běží to do té doby dokud se nevyprázdní fronta
        //       
        //    //a nyní vymažu všechny groupy z listu
        //    foreach(Group gDel in groupForDel){
        //        db.GroupSet.DeleteObject(gDel);
        //    }
        //
        //    db.SaveChanges();
        //    
        //}

        /// <summary>
        /// Vrátí Group podle ID groupy
        /// </summary>
        /// <param name="id">Id které hledáme</param>
        /// <returns></returns>
        public Group FindById(int id) {
            return db.GroupSet.Single(g => g.Id == id);
        }

        /// <summary>
        /// Vrátí Group podle jména groupy
        /// </summary>
        /// <param name="name">Jméno které hledáme</param>
        /// <returns></returns>
        public Group FindByName(string name)
        {
            return db.GroupSet.Single(g => g.Name == name);
        }


		/// <summary>
		/// 
		/// </summary>
		/// <param name="group"></param>
		public void Edit(Group group)
		{
			db.SaveChanges();
		}
	
        #endregion
	}
}