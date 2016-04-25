using System.Globalization;
using System.Text;
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

    using System.Drawing.Imaging;

    using Image = ImageGallery.Data.Models.Image;

    public class ImageService : IImageService
    {
        private IRepository<Album, int> albums;

        private IRepository<Image, int> images;

        private int width;
        private int height;

        private int midwidth;
        private int midheight;

        private int lowwidth;
        private int lowheight;

        private DateTime dateTaken;

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

            using (ImageFactory imageFactory = new ImageFactory(preserveExifData: true))
            {
                this.width = imageFactory.Load(file.InputStream).Image.Width;
                this.height = imageFactory.Load(file.InputStream).Image.Height;
                var a = imageFactory.Load(file.InputStream).ExifPropertyItems[36867];

                //Convert date taken metadata to a DateTime object
                string sdate = Encoding.UTF8.GetString(a.Value).Trim();
                string secondhalf = sdate.Substring(sdate.IndexOf(" "), (sdate.Length - sdate.IndexOf(" ")));
                string firsthalf = sdate.Substring(0, 10);
                firsthalf = firsthalf.Replace(":", "-");
                sdate = firsthalf + secondhalf;
                this.dateTaken = DateTime.Parse(sdate);
            }            

            var newFilename = this.dateTaken.ToString("yyyy-MM-dd-HH-mm-ss-", CultureInfo.CreateSpecificCulture("en-US")) + Guid.NewGuid() + Path.GetExtension(file.FileName);

            // Create initial folders if not available
            FileService.CreateInitialFolders(albumId, server);
            FileService.Save(file.InputStream, ImageType.Original, newFilename, albumId, server);
            this.Resize(file.InputStream, ImageType.Medium, albumId, newFilename, server);
            this.Resize(file.InputStream, ImageType.Low, albumId, newFilename, server);

            GC.Collect();

            var newDbImage = new Data.Models.Image()
                                 {
                                     AlbumId = albumId, 
                                     Title = "aaaaaaaaaaaaaaaaaa", 
                                     Description = "aaaaaaaaaaaaaaaa", 
                                     OriginalFileName = originalFilename, 
                                     FileName = newFilename, 
                                     ImageIdentificator = 1,
                                     Width = this.width,
                                     Height = this.height,
                                     LowHeight = this.lowheight,
                                     LowWidth = this.lowwidth,
                                     MidHeight = this.midheight,
                                     MidWidth = this.midwidth,
                                     DateTaken = this.dateTaken
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
                        this.lowwidth = imageFactory.Load(outStream).Image.Width;
                        this.lowheight = imageFactory.Load(outStream).Image.Height;
                    }
                    else if (type == ImageType.Medium)
                    {
                        imageFactory.Load(inputStream)
                            .Resize(new ResizeLayer(new Size(Common.Constants.ImageMiddleMaxSize, Common.Constants.ImageMiddleMaxSize), ResizeMode.Max))
                            .Format(new JpegFormat { Quality = 70 })
                            .Save(outStream);
                        this.midwidth = imageFactory.Load(outStream).Image.Width;
                        this.midheight = imageFactory.Load(outStream).Image.Height;
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