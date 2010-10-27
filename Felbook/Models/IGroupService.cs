using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Felbook.Models
{
    public interface IGroupService
    {
        IQueryable<User> GetUsers(Group grp);
        void Add(Group grp); //přidání nové skupiny
        void Delete(Group grp); //zatím ještě nefunguje pořádně
        void AddSubGroup(int idParrentGroup, Group child); //přidání podskupiny do skupiny
        Group SearchById(int id); //najde skupinu podle ID
    }
}
