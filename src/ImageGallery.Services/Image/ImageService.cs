using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using ImageGallery.Data.Common;

namespace ImageGallery.Services.Image
{
    public class ImageService : IImageService
    {
        private IRepository<Data.Models.Image, int> images;
        private IRepository<Data.Models.Album, int> albums;

        public ImageService(IRepository<Data.Models.Image, int> images, IRepository<Data.Models.Album, int> albums)
        {
            this.images = images;
            this.albums = albums;
        }

        public IQueryable<Data.Models.Image> GetAll()
        {
            return this.images.All();
        }

        public Data.Models.Image GetById(int id)
        {
            return this.GetAll().FirstOrDefault(x => x.Id == id);
        }

        public void Add(int albumId, HttpPostedFileBase file, HttpServerUtility server)
        {
            var counter = 0;

            if (file == null)
            {
                return;
            }

            if (!Directory.Exists(server.MapPath(Common.Constants.MainContentFolder)))
            {
                Directory.CreateDirectory(server.MapPath(Common.Constants.MainContentFolder));
            }

            if (!Directory.Exists(server.MapPath(Common.Constants.MainContentFolder + "\\" + albumId)))
            {
                Directory.CreateDirectory(server.MapPath(Common.Constants.MainContentFolder + "\\" + albumId));
            }

            if (!Directory.Exists(server.MapPath(Common.Constants.MainContentFolder + "\\" + albumId + "\\" + Common.Constants.ImageFolderOriginal)))
            {
                Directory.CreateDirectory(server.MapPath(Common.Constants.MainContentFolder + "\\" + albumId + "\\" + Common.Constants.ImageFolderOriginal));
            }

            if (!Directory.Exists(server.MapPath(Common.Constants.MainContentFolder + "\\" + albumId + "\\" + Common.Constants.ImageFolderMiddle)))
            {
                Directory.CreateDirectory(server.MapPath(Common.Constants.MainContentFolder + "\\" + albumId + "\\" + Common.Constants.ImageFolderMiddle));
            }

            if (!Directory.Exists(server.MapPath(Common.Constants.MainContentFolder + "\\" + albumId + "\\" + Common.Constants.ImageFolderLow)))
            {
                Directory.CreateDirectory(server.MapPath(Common.Constants.MainContentFolder + "\\" + albumId + "\\" + Common.Constants.ImageFolderLow));
            }

        
            var lastImage = this.albums.GetById(albumId).Images.LastOrDefault();

            if (lastImage == null)
            {
                counter = 0;
            }
            else
            {
                counter = lastImage.ImageIdentificator;
            }

            var originalFilename = Path.GetFileName(file.FileName);
            var newFilename = (counter + 1).ToString().PadLeft(4, '0') + ".jpg";
            var originalPath = server.MapPath(Common.Constants.MainContentFolder + "\\" + albumId + "\\" + Common.Constants.ImageFolderOriginal + "\\");
            var middlePath = server.MapPath(Common.Constants.MainContentFolder + "\\" + albumId + "\\" + Common.Constants.ImageFolderMiddle + "\\");
            var lowPath = server.MapPath(Common.Constants.MainContentFolder + "\\" + albumId + "\\" + Common.Constants.ImageFolderLow + "\\");
            file.SaveAs(originalPath + newFilename);
            
            this.ScaleImage(new Bitmap(originalPath + newFilename), Common.Constants.ImageMiddleMaxSize, Common.Constants.ImageMiddleMaxSize).Save(middlePath + newFilename, ImageFormat.Jpeg);
            this.ScaleImage(new Bitmap(originalPath + newFilename), Common.Constants.ImageLowMaxSize, Common.Constants.ImageLowMaxSize).Save(lowPath + newFilename, ImageFormat.Jpeg);

            var newDbImage = new Data.Models.Image()
            {
                AlbumId = albumId,
                Title = "aaaaaaaaaaaaaaaaaa",
                Description = "aaaaaaaaaaaaaaaa",
                OriginalFileName = originalFilename,
                FileName = newFilename,
                ImageIdentificator = counter + 1
            };
        
            this.images.Add(newDbImage);            
        }

        private System.Drawing.Image ScaleImage(System.Drawing.Image image, int maxWidth, int maxHeight)
        {
            var ratioX = (double)maxWidth / image.Width;
            var ratioY = (double)maxHeight / image.Height;
            var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);

            var newImage = new Bitmap(newWidth, newHeight);

            using (var graphics = Graphics.FromImage(newImage))
                graphics.DrawImage(image, 0, 0, newWidth, newHeight);

            return newImage;
        }
    }
}
