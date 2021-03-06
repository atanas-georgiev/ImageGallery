﻿using System.Globalization;
using System.Text;
using System.Threading;
using ImageGallery.Common;
using ImageGallery.Services.File;
using ImageProcessor;
using ImageProcessor.Imaging;
using ImageProcessor.Imaging.Formats;
using MetadataExtractor;
using MetadataExtractor.Formats.Exif;

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
        private IRepository<Album, Guid> albums;

        private IRepository<Image, Guid> images;
        
        private int midwidth;
        private int midheight;

        private int lowwidth;
        private int lowheight;

        private DateTime dateTaken;

        public ImageService(IRepository<Image, Guid> images, IRepository<Album, Guid> albums)
        {
            this.images = images;
            this.albums = albums;
        }

        public void Add(Guid albumId, HttpPostedFileBase file, HttpServerUtility server)
        {
            if (file == null)
            {
                return;
            }

            Data.Models.Image image = ExtractExifData(file.InputStream);

//            string test = "";
//
//            while (test == "")
//            {
//                test = TestGeo.RetrieveFormatedAddress("42.697626", "23.322284");
//                Thread.Sleep(500);
//            }

            if (image.FileName != null)
            {
                image.FileName += Path.GetExtension(file.FileName);
            }

            image.OriginalFileName = Path.GetFileName(file.FileName);
                   
            // Create initial folders if not available
            FileService.CreateInitialFolders(albumId, server);
            FileService.Save(file.InputStream, ImageType.Original, image.FileName, albumId, server);
            this.Resize(file.InputStream, ImageType.Medium, albumId, image.FileName, server);
            this.Resize(file.InputStream, ImageType.Low, albumId, image.FileName, server);

            GC.Collect();

            image.AlbumId = albumId;
            image.Title = "aaaaaaaaaaaaaaaaaa";
            image.Description = "aaaaaaaaaaaaaaaa";
            image.LowHeight = this.lowheight;
            image.LowWidth = this.lowwidth;
            image.MidHeight = this.midheight;
            image.MidWidth = this.midwidth;
           
            this.images.Add(image);            
        }

        private Data.Models.Image ExtractExifData(Stream inputStream)
        {
            var newImage = new Data.Models.Image();

            using (ImageFactory imageFactory = new ImageFactory(preserveExifData: true))
            {
                var imageFactoryStream = ImageMetadataReader.ReadMetadata(inputStream);
                var subIfdDirectory = imageFactoryStream.OfType<ExifSubIfdDirectory>().FirstOrDefault();
                var subIfdDirectory2 = imageFactoryStream.OfType<ExifIfd0Directory>().FirstOrDefault();

                try
                {
                    newImage.DateTaken = subIfdDirectory?.GetDateTime(ExifDirectoryBase.TagDateTimeOriginal);
                }
                catch
                {
                    newImage.DateTaken = subIfdDirectory?.GetDateTime(ExifDirectoryBase.TagDateTime);
                }

                try
                {
                    newImage.FileName =
                        subIfdDirectory?.GetDateTime(ExifDirectoryBase.TagDateTimeOriginal)
                            .ToString("yyyy-MM-dd-HH-mm-ss-", CultureInfo.CreateSpecificCulture("en-US")) +
                        Guid.NewGuid();
                }
                catch
                {
                }

                try
                {
                    newImage.Iso = subIfdDirectory?.GetInt32(ExifDirectoryBase.TagIsoEquivalent);
                }
                catch
                {
                    // ignored
                }

                try
                {
                    newImage.ShutterSpeed = subIfdDirectory?.GetDescription(ExifDirectoryBase.TagShutterSpeed);
                }
                catch
                {
                }

                try
                {
                    newImage.Aperture = subIfdDirectory?.GetDescription(ExifDirectoryBase.TagAperture);
                }
                catch
                {
                }

                try
                {
                    newImage.FocusLen = subIfdDirectory?.GetDouble(ExifDirectoryBase.TagFocalLength);
                }
                catch
                {
                }

                try
                {
                    newImage.CameraMaker = subIfdDirectory2?.GetString(ExifDirectoryBase.TagMake);
                }
                catch
                {
                }

                try
                {
                    newImage.CameraModel = subIfdDirectory2?.GetString(ExifDirectoryBase.TagModel);
                }
                catch
                {
                }

                try
                {
                    newImage.ExposureBiasStep = subIfdDirectory?.GetDouble(ExifDirectoryBase.TagExposureBias);
                }
                catch
                {
                }

                try
                {
                    newImage.Width = subIfdDirectory.GetInt32(ExifDirectoryBase.TagExifImageWidth);
                }
                catch
                {
                }

                try
                {
                    newImage.Height = subIfdDirectory.GetInt32(ExifDirectoryBase.TagExifImageHeight);
                }
                catch
                {
                }

                try
                {
                    newImage.Lenses = subIfdDirectory.GetString(ExifDirectoryBase.TagLensModel);
                }
                catch
                {
                }
            }

            return newImage;
        }

        private void Resize(Stream inputStream, ImageType type, Guid albumId, string originalFilename, HttpServerUtility server)
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

        public Image GetById(Guid id)
        {
            return this.GetAll().FirstOrDefault(x => x.Id == id);
        }

        public void Update(Image image)
        {
            this.images.Update(image);
        }

        public void Remove(Guid id)
        {
            this.images.Delete(id);
        }
    }
}