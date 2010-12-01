using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Felbook.Models
{
    public interface IModel
    {
        IGroupService GroupService{ get; }
        IUserService UserService { get; }
        IMessageService MessageService { get; }
        IWallService WallService { get; }
        IStatusService StatusService { get; }
		IFileService FileService { get; }
		IImageService ImageService { get; }
        IEventService EventService { get; }
    }
    
    public class Model : IModel
	{
		#region Variables

		private IGroupService groupService;

		private IUserService userService;

        private IMessageService messageService;

		private IWallService wallService;

		private IImageService imageService;

		private IStatusService statusService;

		private IFileService fileService;

        private IEventService eventService;

		#endregion

		#region Properties

		protected FelBookDBEntities DBEntities { get; set; }

		public IGroupService GroupService
		{
			get
			{
				if (groupService == null)
				{
					groupService = new GroupService(DBEntities, WallService);
				}

				return groupService;
			}
		}

		public IUserService UserService
		{
			get
			{
				if (userService == null)
				{
					userService = new UserService(DBEntities, WallService);
				}

				return userService;
			}
		}

        public IMessageService MessageService
        {
            get
            {
                if (messageService == null)
                {
                    messageService = new MessageService(DBEntities);
                }

                return messageService;
            }
        }

		public IWallService WallService
		{
			get
			{
				if (wallService == null)
				{
					wallService = new WallService(DBEntities);
				}

				return wallService;
			}
		}
		
		public IImageService ImageService
		{
			get
			{
				if (imageService == null)
				{
					imageService = new ImageService(DBEntities);
					imageService.ImageDir = "/Web_Data/status_images";
					imageService.MaxHeight = 600;
					imageService.MaxWidth = 800;
					imageService.MaxThumbnailHeight = 60;
					imageService.MaxThumbnailWidth = 80;
				}

				return imageService;
			}
		}

		public IFileService FileService
		{
			get
			{
				if (fileService == null)
				{
					fileService = new FileService(DBEntities);
					fileService.FileDir = "/Web_Data/status_files";
				}

				return fileService;
			}
		}

		public IStatusService StatusService
		{
			get
			{
				if (statusService == null)
				{
					statusService = new StatusService(DBEntities, WallService, ImageService, FileService);
				}
				return statusService;
			}
		}

        public IEventService EventService 
        {
            get 
            {
                if (eventService == null) 
                {
                    eventService = new EventService(DBEntities);
                }
                return eventService;
            }
        }

		#endregion

		public Model()
		{
			DBEntities = new FelBookDBEntities();
		}
	}
}