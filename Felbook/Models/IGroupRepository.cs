using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Felbook.Models
{
    public interface IGroupRepository
    {
        
        IQueryable<User> GetUsers(Group grp);        
        void Add(Group grp);
        void Delete(Group grp); //zatím ještě nefunguje pořádně
        void AddSubGroup(Group parent, Group child); //přidání podskupiny do skupiny
        Group SearchById(int id); //najde skupinu podle ID

        void Save();
    }
}
