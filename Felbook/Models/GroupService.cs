using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Felbook.Models
{
    public class GroupService : IGroupService
    {
        #region Proměnné
        private FelBookDBEntities db = new FelBookDBEntities();
        #endregion

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
        /// Přidání nové podgrupy k dané nadgrupě
        /// </summary>
        /// <param name="idParrentGroup">ID groupy do které budu přidávat podgrupu</param>
        /// <param name="child">nová podgrupa</param>
        public void AddSubGroup(int idParrentGroup, Group child) {
            //vytáhnu si grupu podle ID
            Group grp = SearchById(idParrentGroup);
            //přidám dítě do nadgrupy
            grp.Children.Add(child);
            //podgrupě nastavím rodiče jako nadgrupu
            child.Parent = grp;
            //přidám podgrupu mezi ostatní grupy
            db.AddToGroupSet(child);
        }

        /// <summary>
        /// Přidání nové skupiny
        /// </summary>
        /// <param name="grp">skupina</param>
        public void Add(Group grp)
        {
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
            
        }

        /// <summary>
        /// Vrátí Group podle ID groupy
        /// </summary>
        /// <param name="id">Id které hledáme</param>
        /// <returns></returns>
        public Group SearchById(int id) {
            return db.GroupSet.Single(g => g.Id == id);
        }


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