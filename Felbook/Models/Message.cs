using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Felbook.Models
{
    public partial class Message : IComparable<Message>
    {

        public int CompareTo(Message other)
        {

            if (this.Created == other.Created)
            {
                return 0;
            }
            
            if (this.Created < other.Created)
            {
                return -1;
            }
            else
            {
                return 1;
            }

        }

    }

}