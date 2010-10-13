using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Felbook.Models
{
    public interface IGroupRepository
    {
        bool IsInGroup(User usr, Group grp);
        IQueryable<User> GetUsers(Group grp);
        IQueryable<Information> GetInformations(Group grp);
        void AddInformation(Group grp, Information info);
        
        //void DeleteInformation(Information info); //zatím nevím protože nevím jak mazat rekurzivně
        void SetInformation(int idInformation, Information newInfo);
        void Add(Group grp);
        //void Delete(Group grp); //zatím nevím protože nevím jak mazat rekurzivně

        void Save();
    }
}
