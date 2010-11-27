using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Felbook.Models;

namespace Felbook.Tests.Fakes
{
    class MockImageService : IImageService
    {
        public string GetImagePath(Image image)
        {
            throw new NotImplementedException();
        }

        public string GetImageSrc(Image image)
        {
            throw new NotImplementedException();
        }

        public int GetImageThumbnailHeight(Image image)
        {
            throw new NotImplementedException();
        }

        public int GetImageThumbnailWidth(Image image)
        {
            throw new NotImplementedException();
        }

        public string GetThumbnailPath(Image image)
        {
            throw new NotImplementedException();
        }

        public string GetThumbnailSrc(Image image)
        {
            throw new NotImplementedException();
        }

        public string ImageDir
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public int MaxHeight
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public int MaxThumbnailHeight
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public int MaxThumbnailWidth
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public int MaxWidth
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public void SaveImage(Image image, System.IO.Stream inputStream)
        {
            throw new NotImplementedException();
        }
    }
}
