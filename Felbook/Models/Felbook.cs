using System.Collections.Generic;
using System.Linq;

namespace Felbook.Models
{
    partial class SimpleInformation
    {

        public override string getContent()
        {

            if (canUserReadMe(loggedUserID))
            {
                return Content;
            }
            else
            {
                return "";
            }

        }

    }

    partial class StructuredInformation
    {

        Felbook.Models.FelbookDataContext db = new Models.FelbookDataContext();

        public override string getContent()
        {
            if (canUserReadMe(loggedUserID))
            {

                string output = "";
                
                IEnumerator<Information> iter = db.Informations.Where(i => i.ParentInfoID == InfoID).GetEnumerator();

                while (iter.MoveNext())
                {
                    output = output + iter.Current.getContent() + "\n";

                }

                return output;

            }
            else
            {
                return "";
            }
        }

    }

    partial class Information
    {

        static protected int loggedUserID = 1;
        
        protected bool canUserReadMe(int userID)
        {
            
            Felbook.Models.FelbookDataContext db = new Models.FelbookDataContext();

            return db.Rights.Single(r => r.UserID == userID && r.InfoID == InfoID).CanRead;

        }

        public abstract string getContent();
        
    }

    
    partial class FelbookDataContext
    {
    }
}
