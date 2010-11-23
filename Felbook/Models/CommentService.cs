using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Felbook.Models
{
    public interface IStatusService
    {
        // Najde status, která má odpovídající id
        Status FindStatusById(int id);

        // Vytvoří nový komentář se zadanými parametry
        void AddCommentToStatus(User author, Status commentedStatus, string text);

    }
    
    public class StatusService : IStatusService
    {

        #region Properities

        private FelBookDBEntities db { get; set; }
                
        #endregion

        #region Construcotrs

        public StatusService(FelBookDBEntities DBEntities)
        {
            db = DBEntities;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Najde status, který má odpovídající id
        /// </summary>
        /// <param name="author">id statusu, který má být vyhledán</param>
        /// <returns>status, který má odpovídající id</returns>
        public Status FindStatusById(int id)
        {
            try
            {
                return db.StatusSet.Single(m => m.Id == id);
            }
            catch (InvalidOperationException)
            {
                return null;
            }
        }

        /// <summary>
        /// Vytvoří nový komentář ke statusu se zadanými parametry
        /// </summary>
        /// <param name="author">autor komentáře</param>
        /// <param name="commentedStatus">status, který je komentován</param>
        /// <param name="text">text komentáře</param>
        public void AddCommentToStatus(User author, Status commentedStatus, string text)
        {
            //if (commentedStatus != null)
            //{
                Comment comm = new Comment();
                comm.Author = author;
                comm.CommentStatus = commentedStatus;
                comm.Text = text;
                comm.Created = DateTime.Now;
                db.CommentSet.AddObject(comm);
                db.SaveChanges();
            //}
        }

        #endregion

    }
}