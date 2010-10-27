using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Felbook.Models
{
    public interface ISystemService
    {
        IQueryable<User> SearchUsers(string str);
        IQueryable<Group> SearchGroups(string str);
    }
}
