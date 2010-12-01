using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Felbook.Helpers;

namespace Felbook.Models
{
    public interface IStatusService
    {
        // Najde status, která má odpovídající id
        Status FindStatusById(int id);

        // Vytvoří nový komentář se zadanými parametry
        void AddCommentToStatus(User author, Status commentedStatus, string text);
		
		void AddStatus(User user, Group group, StatusFormModel formModel);
		
		void AddStatus(User user, StatusFormModel formModel);

    }
    
    public class StatusService : IStatusService
    {
		private IWallService wallService;
		private IImageService imageService;
		private IFileService fileService;
		private FelBookDBEntities db;

        #region Constructor

		public StatusService(FelBookDBEntities DBEntities, IWallService wallService, IImageService imageService, IFileService fileService)
		{
			this.db = DBEntities;
			this.wallService = wallService;
			this.imageService = imageService;
			this.fileService = fileService;
		}

        #endregion


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



		public void AddStatus(User user, Group group, StatusFormModel formModel)
		{
			var status = new Status { Text = formModel.Status, User = user, Group = group };
			db.AddToStatusSet(status);

			// obrázky
			if (formModel.Images != null)
			{
				for (int i = 0; i < formModel.Images.Count; i++)
				{
					var uploadedImage = formModel.Images[i];

					// vynechat neobrázky
					if (uploadedImage == null || uploadedImage.ContentLength == 0 || !ImageHelper.IsImage(uploadedImage.ContentType))
					{
						continue;
					}

					// vyrobit entitu
					var img = new Image {
						Description = formModel.ImageDescriptions[i],
					};
					db.ImageSet.AddObject(img);

					status.Images.Add(img);

					// uložit db, aby entita měla id a tudíž se dala vymyslet cesta k obrázku
					db.SaveChanges();

					// uložit soubor
					imageService.SaveImage(img, uploadedImage.InputStream);
				}
			}

			// soubory
			if (formModel.Files != null)
			{
				for (int i = 0; i < formModel.Files.Count; i++)
				{
					var uploadedFile = formModel.Files[i];

					if (uploadedFile == null || uploadedFile.ContentLength == 0)
					{
						continue;
					}

					// vyrobit entitu
					var file = new File {
						Description = formModel.FileDescriptions[i],
						FileName = uploadedFile.FileName,
					};
					db.FileSet.AddObject(file);

					status.Files.Add(file);

					// uložit db, aby entita měla id a tudíž se dala vymyslet cesta k souboru
					db.SaveChanges();

					// uložit soubor
					fileService.SaveFile(file, uploadedFile);
				}
			}

			// odkazy
			if (formModel.Links != null)
			{
				for (int i = 0; i < formModel.Links.Count; i++)
				{
					var url = formModel.Links[i];

					if (String.IsNullOrWhiteSpace(url))
					{
						continue;
					}

					status.Links.Add(new Link {
						URL = url,
						Description = formModel.LinkDescriptions[i],
					});
				}
			}

			db.SaveChanges();

			// přidat do zdí
			List<User> userList = user.Followers.ToList();
			
			if (group != null)
			{
				userList.Union(group.Users);
			}
			else
			{
				userList.Add(user);
			}

			wallService.Add(status, userList.Distinct());
		}



		public void AddStatus(User user, StatusFormModel formModel)
		{
			AddStatus(user, null, formModel);
		}

	}



	public class StatusFormModel
	{
		[Required(ErrorMessage = "Status is not set")]
		[DisplayName("Status text")]
		public string Status { get; set; }

		// TODO validate image
		public IList<HttpPostedFileBase> Images { get; set; }
		public IList<string> ImageDescriptions { get; set; }

		public IList<HttpPostedFileBase> Files { get; set; }
		public IList<string> FileDescriptions { get; set; }

		[RegularExpression(@"^(http|https|ftp)\://+[a-zA-Z]{2,3}(:[a-zA-Z0-9]*)?/?([a-zA-Z0-9\-\._\?\,\'/\\\+&amp;%\$#\=~])*[^\.\,\)\(\s]$")]
		public IList<string> Links { get; set; }
		public IList<string> LinkDescriptions { get; set; }
	}

}