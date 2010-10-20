using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Felbook.Models
{
    /// <summary>
    /// Model týkající se operací které můžou dělat věškerý klienti, takže i ti co nejsou přihlášeni
    /// </summary>
    public class SystemRepository : ISystemRepository
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
        public IQueryable<User> SearchUsers(string str) {
            return from users in db.UserSet
                   where users.Name.Contains(str) || users.Surname.Contains(str)
                   select users;
        }

        /// <summary>
        /// Vrátí skupiny ve kterých se v názvech objevuje daný řetězec
        /// </summary>
        /// <param name="str">Řetězec pomocí kterého hledáme</param>
        /// <returns>recordset skupin</returns>
        public IQueryable<Group> SearchGroups(string str){
            return from groups in db.GroupSet
                   where groups.Name.Contains(str) ||
                   groups.Description.Contains(str)
                   select groups;
        }

        #endregion
    
        #region Insert test data
            public void InsertTestData()
        {
            #region Create users
            User usr1 = new User();
            usr1.Id = 1;
            usr1.Name = "Karel";
            usr1.Surname = "Novák";
            usr1.Created = new DateTime(2008, 3, 1, 7, 0, 0);
            usr1.Mail = "karel@novak.cz";

            User usr2 = new User();
            usr2.Id = 2;
            usr2.Name = "Jan";
            usr2.Surname = "Horák";
            usr2.Created = new DateTime(2009, 9, 10, 10, 0, 0);
            usr2.Mail = "jan@horak.cz";

            User usr3 = new User();
            usr3.Id = 3;
            usr3.Name = "Filip";
            usr3.Surname = "Nový";
            usr3.Created = new DateTime(2010, 1, 1, 10, 10, 10);
            usr3.Mail = "filip@novy.cz";

            User usr4 = new User();
            usr4.Id = 4;
            usr4.Name = "Ondřej";
            usr4.Surname = "Zelený";
            usr4.Created = new DateTime(2004, 2, 2, 8, 8, 5);
            usr4.Mail = "ondrej@zeleny.cz";

            User usr5 = new User();
            usr5.Id = 5;
            usr5.Name = "Ondřej";
            usr5.Surname = "Zelený";
            usr5.Created = new DateTime(2007, 5, 1, 5, 1, 5);
            usr5.Mail = "ondrej@zeleny.cz";

            db.AddToUserSet(usr1);
            db.AddToUserSet(usr2);
            db.AddToUserSet(usr3);
            db.AddToUserSet(usr4);
            db.AddToUserSet(usr5);
            
            #endregion

            #region Create messages
            Message msg1 = new Message();
            msg1.Id = 1;
            msg1.Sender = usr1;
            msg1.Users.Add(usr2);
            msg1.Text = "Ahoj jak se máš?";

            Message msg2 = new Message();
            msg2.Id = 2;
            msg2.Sender = usr1;
            msg2.Users.Add(usr2);
            msg2.Users.Add(usr3);
            msg2.Users.Add(usr4);
            msg2.Users.Add(usr5);
            msg2.Text = "Nezapomeňte dodělat domácí úkol.";

            Message msg3 = new Message();
            msg3.Id = 3;
            msg3.Sender = usr3;
            msg3.Users.Add(usr1);
            msg3.FirstMessage.Add(msg2);
            msg3.Text = "Už jsem ten úkol odevzdal.";

            Message msg4 = new Message();
            msg4.Id = 4;
            msg4.Sender = usr3;
            msg4.Users.Add(usr1);
            msg4.Users.Add(usr2);
            msg4.Text = "Díky za ty materiály, zejtra vás zvu na pivo.";

            Message msg5 = new Message();
            msg5.Id = 5;
            msg5.Sender = usr5;
            msg5.Users.Add(usr4);
            msg5.Text = "Podařilo se mi rozchodit ten server !!!";

            db.AddToMessageSet(msg1);
            db.AddToMessageSet(msg2);
            db.AddToMessageSet(msg3);
            db.AddToMessageSet(msg4);
            db.AddToMessageSet(msg5);
            
            #endregion
            
            #region create groups
            Group grp1 = new Group();
            grp1.Id = 1;
            grp1.Name = "Svět";

            Group grp2 = new Group();
            grp2.Id = 2;
            grp2.Name = "Asie";

            Group grp3 = new Group();
            grp3.Id = 3;
            grp3.Name = "Evropa";

            Group grp4 = new Group();
            grp4.Id = 4;
            grp4.Name = "Česká republika";

            Group grp5 = new Group();
            grp5.Id = 5;
            grp5.Name = "Slunce";

            #endregion

                //ještě přidám
            #region create statuses
            Status st1 = new Status();
            st1.Id = 1;
            st1.Text = "Dneska jsem udělal maturitu";
            st1.User = usr1;

            Status st2 = new Status();
            st2.Id = 2;
            st2.Text = "Podařilo se mi tu maturitu dát s vyznamenáním";
            st2.User = usr1;    

            #endregion
                
                //ještě přidám
            #region create comments
            Comment com1 = new Comment();
            com1.Text = "Na jakou se budeš hlásit vejšku?";
            st2.Comments.Add(com1);
            
            #endregion

                //ještě dodělám obrázky, linky atd

            db.SaveChanges();
        }
        #endregion
    }
}