using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Felbook.Models
{
	public class TestData
	{
		public static void Insert()
		{

			#region Insert test data

			FelBookDBEntities db = new FelBookDBEntities();

			#region Vymazání všech tabulek a resetování ID

			//vymazání tabulek které spojují
            db.ExecuteStoreCommand("DELETE FROM StatusInformationFiles");
            db.ExecuteStoreCommand("DELETE FROM StatusInformationImages");
            db.ExecuteStoreCommand("DELETE FROM StatusInformationLinks");
            db.ExecuteStoreCommand("DELETE FROM Followings");
            db.ExecuteStoreCommand("DELETE FROM GroupAdministration");
            db.ExecuteStoreCommand("DELETE FROM MessageReaders");
            db.ExecuteStoreCommand("DELETE FROM UserGroupMembership");

			//vymazání tabulek které obsahují informace
			db.ExecuteStoreCommand("DELETE FROM ImageSet");
			db.ExecuteStoreCommand("DELETE FROM FileSet");
			db.ExecuteStoreCommand("DELETE FROM LinkSet");
			db.ExecuteStoreCommand("DELETE FROM StatusSet");
			db.ExecuteStoreCommand("DELETE FROM MessageSet");
			db.ExecuteStoreCommand("DELETE FROM GroupSet");
			db.ExecuteStoreCommand("DELETE FROM UserSet");
			db.ExecuteStoreCommand("DELETE FROM CommentSet");

			//resetování ID -> je spuštěno pouze na tabulky které mají klíč typu UNIQUE, INCREMENT=YES
			db.ExecuteStoreCommand("DBCC CHECKIDENT (CommentSet, RESEED, 0)");
			db.ExecuteStoreCommand("DBCC CHECKIDENT (FileSet, RESEED, 0)");
			db.ExecuteStoreCommand("DBCC CHECKIDENT (GroupSet, RESEED, 0)");
			db.ExecuteStoreCommand("DBCC CHECKIDENT (ImageSet, RESEED, 0)");
			db.ExecuteStoreCommand("DBCC CHECKIDENT (LinkSet, RESEED, 0)");
			db.ExecuteStoreCommand("DBCC CHECKIDENT (MessageSet, RESEED, 0)");
			db.ExecuteStoreCommand("DBCC CHECKIDENT (StatusSet, RESEED, 0)");
			db.ExecuteStoreCommand("DBCC CHECKIDENT (UserSet, RESEED, 0)");


			#endregion

			#region naplnění UserSet

			User usr1 = new User();
			usr1.Name = "Jakub";
			usr1.Surname = "Novák";
			usr1.Created = new DateTime(2008, 3, 1, 7, 0, 0);
			usr1.LastLogged = DateTime.Now;
			usr1.Mail = "jakub@novak.cz";
			usr1.Username = "novakjakub";
			usr1.ChangePassword("123456");

			User usr2 = new User();
			usr2.Name = "Jan";
			usr2.Surname = "Novák";
			usr2.Created = new DateTime(2009, 9, 10, 10, 0, 0);
			usr2.LastLogged = new DateTime(2010, 10, 10, 10, 0, 0);
			usr2.Mail = "jan@novak.cz";
			usr2.Username = "novakjan";
			usr2.ChangePassword("123456");

			User usr3 = new User();
			usr3.Name = "Bedřich";
			usr3.Surname = "Červený";
			usr3.Created = new DateTime(2007, 4, 8, 11, 5, 4);
			usr3.LastLogged = new DateTime(2008, 7, 8, 9, 4, 3);
			usr3.Mail = "bedrich@cerveny.cz";
			usr3.Username = "bedrich";
			usr3.ChangePassword("asfad");

			User usr4 = new User();
			usr4.Id = 4;
			usr4.Name = "Ondřej";
			usr4.Surname = "Zelený";
			usr4.Created = new DateTime(2004, 2, 2, 8, 8, 5);
			usr4.LastLogged = new DateTime(2006, 7, 8, 9, 4, 3);
			usr4.Mail = "ondrej@zeleny.cz";
			usr4.Username = "ondrej";
			usr4.ChangePassword("asfad");

			User usr5 = new User();
			usr5.Id = 5;
			usr5.Name = "Jiří";
			usr5.Surname = "Mach";
			usr5.Created = new DateTime(2007, 5, 1, 5, 1, 5);
			usr5.LastLogged = new DateTime(2010, 8, 9, 12, 5, 7);
			usr5.Mail = "jiri@mach.cz";
			usr5.Username = "jiri";
			usr5.ChangePassword("asfad");

			db.AddToUserSet(usr1);
			db.AddToUserSet(usr2);
			db.AddToUserSet(usr3);
			db.AddToUserSet(usr4);
			db.AddToUserSet(usr5);

			#endregion

			#region naplnění MessageSet

			Message msg1 = new Message();
			msg1.Sender = usr1;
			msg1.Text = "Ahoj jak se máš?";
			msg1.Users.Add(usr2);
			msg1.Created = new DateTime(2010, 10, 11, 12, 5, 7);

			Message msg2 = new Message();
			msg2.Sender = usr1;
			msg2.Users.Add(usr2);
			msg2.Users.Add(usr3);
			msg2.Users.Add(usr4);
			msg2.Users.Add(usr5);
			msg2.Text = "Nezapomeňte dodělat domácí úkol.";
			msg2.Created = new DateTime(2009, 3, 5, 8, 1, 2);

			Message msg3 = new Message();
			msg3.Sender = usr3;
			msg3.Users.Add(usr1);
			msg3.ReplyTo = msg2;
			msg3.Text = "Už jsem ten úkol odevzdal.";
			msg3.Created = new DateTime(2007, 12, 11, 10, 4, 4);

			Message msg4 = new Message();
			msg4.Sender = usr3;
			msg4.Users.Add(usr1);
			msg3.ReplyTo = msg2;
			msg4.Text = "Taky už jsem ho udělal a díky za ty materiály, zejtra vás zvu na pivo.";
			msg4.Created = new DateTime(2010, 12, 8, 10, 2, 2);

			Message msg5 = new Message();
			msg5.Sender = usr5;
			msg5.Users.Add(usr4);
			msg5.Text = "Podařilo se mi rozchodit ten server !!!";
			msg5.Created = new DateTime(2010, 1, 8, 7, 6, 2);

			db.AddToMessageSet(msg1);
			db.AddToMessageSet(msg2);
			db.AddToMessageSet(msg3);
			db.AddToMessageSet(msg4);
			db.AddToMessageSet(msg5);

			#endregion

			#region naplnění GroupSet
			Group grp1 = new Group();
			Group grp2 = new Group();
			Group grp3 = new Group();
			Group grp4 = new Group();
			Group grp5 = new Group();

			grp1.Name = "Svět";
			grp1.Description = "Řídíme svět";
			grp1.Creator = usr2;
			grp1.Children.Add(grp2);
			grp1.Children.Add(grp3);
			grp1.Users.Add(usr1);
			grp1.Users.Add(usr2);
			grp1.Users.Add(usr3);
			grp1.Users.Add(usr4);
			grp1.Users.Add(usr5);
			usr1.JoinedGroups.Add(grp1);
			usr2.JoinedGroups.Add(grp2);
			grp1.Administrators.Add(usr1);
			grp1.Administrators.Add(usr2);

			grp2.Name = "Asie";
			grp2.Description = "Řídíme asii";
			grp2.Parent = grp1;
			grp2.Creator = usr1;
			grp2.Administrators.Add(usr1);

			grp3.Name = "Evropa";
			grp3.Description = "Řídíme Evropu";
			grp3.Parent = grp2;
			grp3.Children.Add(grp4);
			grp3.Creator = usr1;
			grp3.Administrators.Add(usr1);

			grp4.Name = "Česká republika";
			grp4.Description = "Řídíme Českou republiku";
			grp4.Parent = grp3;
			grp4.Creator = usr1;
			grp4.Administrators.Add(usr1);

			grp5.Name = "Slunce";
			grp5.Description = "Jsme úplně jiná planeta";
			grp5.Creator = usr1;
			grp5.Administrators.Add(usr1);

			#endregion

			#region naplnění StatusSet a zároveň CommentSet
			Status st1 = new Status();
			st1.Text = "Dneska jsem udělal maturitu";
			st1.User = usr1;
			st1.Created = new DateTime(2010, 1, 1, 12, 3, 4);

			Status st2 = new Status();
			st2.Text = "Podařilo se mi tu maturitu dát s vyznamenáním";
			st2.User = usr2;
			st2.Created = new DateTime(2010, 2, 1, 12, 3, 4);

			Status st3 = new Status();
			st3.Text = "Tohle je zajímavá skupina";
			st3.User = usr2;
			st3.Group = grp1;
			st3.Created = new DateTime(2008, 9, 1, 6, 2, 1);

			Status st4 = new Status();
			st4.Text = "Tady v té skupině řešíme evropu";
			st4.User = usr2;
			st4.Group = grp3;
			st4.Created = new DateTime(2009, 10, 2, 4, 8, 8);

			Status st5 = new Status();
			st5.Text = "Kdo neskáče není čech !!";
			st5.User = usr4;
			st5.Group = grp4;
			st5.Created = new DateTime(2010, 2, 4, 7, 1, 2);

			//TOTO nejde zatím nevím proč :(
			/* 
			 Comment cmt1 = new Comment();
			 cmt1.Text = "Ano také si to myslí !!";
			 cmt1.Created = new DateTime(208, 10, 10, 7, 2, 3);
            
			 st3.Comments.Add(cmt1);
			 db.AddToCommentSet(cmt1);
			 */

			db.AddToStatusSet(st1);
			db.AddToStatusSet(st2);
			db.AddToStatusSet(st3);
			db.AddToStatusSet(st4);
			db.AddToStatusSet(st5);

			db.SaveChanges();

			#endregion

			#endregion
		}
	}
}