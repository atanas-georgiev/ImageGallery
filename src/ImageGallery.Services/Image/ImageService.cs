using ImageGallery.Common;
using ImageGallery.Services.File;
using ImageProcessor;
using ImageProcessor.Imaging;
using ImageProcessor.Imaging.Formats;

namespace ImageGallery.Services.Image
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Linq;
    using System.Web;

    using ImageGallery.Data.Common;
    using ImageGallery.Data.Models;

    using Image = ImageGallery.Data.Models.Image;

    public class ImageService : IImageService
    {
        private IRepository<Album, int> albums;

        private IRepository<Image, int> images;

        public ImageService(IRepository<Image, int> images, IRepository<Album, int> albums)
        {
            this.images = images;
            this.albums = albums;
        }

        public void Add(int albumId, HttpPostedFileBase file, HttpServerUtility server)
        {
            if (file == null)
            {
                return;
            }

            var originalFilename = Path.GetFileName(file.FileName);

            // Create initial folders if not available
            FileService.CreateInitialFolders(albumId, server);
            FileService.Save(file.InputStream, ImageType.Original, originalFilename, albumId, server);
            this.Resize(file.InputStream, ImageType.Medium, albumId, originalFilename, server);
            this.Resize(file.InputStream, ImageType.Low, albumId, originalFilename, server);            

            GC.Collect();

            var newDbImage = new Data.Models.Image()
                                 {
                                     AlbumId = albumId, 
                                     Title = "aaaaaaaaaaaaaaaaaa", 
                                     Description = "aaaaaaaaaaaaaaaa", 
                                     OriginalFileName = originalFilename, 
                                     FileName = originalFilename, 
                                     ImageIdentificator = 1
                                 };

            this.images.Add(newDbImage);
        }

        private void Resize(Stream inputStream, ImageType type, int albumId, string originalFilename, HttpServerUtility server)
        {
            inputStream.Seek(0, SeekOrigin.Begin);

            using (MemoryStream outStream = new MemoryStream())
            {                
                using (ImageFactory imageFactory = new ImageFactory(preserveExifData: true))
                {
                    if (type == ImageType.Low)
                    {
                        imageFactory.Load(inputStream)
                            .Resize(new ResizeLayer(new Size(Common.Constants.ImageLowMaxSize, Common.Constants.ImageLowMaxSize), ResizeMode.Max))
                            .Format(new JpegFormat { Quality = 70 })
                            .Save(outStream);
                    }
                    else if (type == ImageType.Medium)
                    {
                        imageFactory.Load(inputStream)
                            .Resize(new ResizeLayer(new Size(Common.Constants.ImageMiddleMaxSize, Common.Constants.ImageMiddleMaxSize), ResizeMode.Max))
                            .Format(new JpegFormat { Quality = 70 })
                            .Save(outStream);
                    }
                    FileService.Save(outStream, type, originalFilename, albumId, server);
                }
            }
        }

        public IQueryable<Image> GetAll()
        {
            return this.images.All();
        }

        public Image GetById(int id)
        {
            return this.GetAll().FirstOrDefault(x => x.Id == id);
        }
    }
}