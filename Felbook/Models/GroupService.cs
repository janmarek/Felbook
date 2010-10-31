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
        #endregion

		#region Konstruktor

		public GroupService(FelBookDBEntities DBEntities)
		{
			db = DBEntities;
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
        /// <param name="idParrentGroup">ID groupy do které budu přidávat podgrupu</param>
        /// <param name="child">nová podgrupa</param>
        public void AddSubGroup(Group group, Group child) {
            //přidám dítě do nadgrupy
			group.Children.Add(child);
            //podgrupě nastavím rodiče jako nadgrupu
			child.Parent = group;
            //přidám podgrupu mezi ostatní grupy
            db.AddToGroupSet(child);
			db.SaveChanges();
        }

        /// <summary>
        /// Přidání nové skupiny
        /// </summary>
        /// <param name="grp">skupina</param>
        public void Add(Group grp)
        {
            db.AddToGroupSet(grp);
			db.SaveChanges();
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
        public void Delete(Group grp)
        {
            List<Group> groupForDel = new List<Group>();
            Queue<Group> Q = new Queue<Group>();
            Group g;
            Q.Enqueue(grp);
            do
            {
                g = Q.Dequeue();
                groupForDel.Add(g);
                for (int i = 0; i < g.Children.Count; i++)
                {
                    Q.Enqueue(g.Children.ElementAt(i));
                }
            } while (Q.Count > 0); //běží to do té doby dokud se nevyprázdní fronta
               
            //a nyní vymažu všechny groupy z listu
            foreach(Group gDel in groupForDel){
                db.GroupSet.DeleteObject(gDel);
            }

			db.SaveChanges();
            
        }

        /// <summary>
        /// Vrátí Group podle ID groupy
        /// </summary>
        /// <param name="id">Id které hledáme</param>
        /// <returns></returns>
        public Group FindById(int id) {
            return db.GroupSet.Single(g => g.Id == id);
        }
        #endregion
    }
}