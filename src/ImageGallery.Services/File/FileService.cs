using System;
using System.IO;
using System.Web;
using ImageGallery.Common;

namespace ImageGallery.Services.File
{
    static class FileService
    {
        static string GetImageFolder(int albumId, ImageType type, HttpServerUtility server)
        {
            switch (type)
            {
                case ImageType.Low:
                    return server.MapPath(Common.Constants.MainContentFolder + "\\" + albumId + "\\" + Common.Constants.ImageFolderLow + "\\");
                case ImageType.Medium:
                    return server.MapPath(Common.Constants.MainContentFolder + "\\" + albumId + "\\" + Common.Constants.ImageFolderMiddle + "\\");
                case ImageType.Original:
                    return server.MapPath(Common.Constants.MainContentFolder + "\\" + albumId + "\\" + Common.Constants.ImageFolderOriginal + "\\");
            }
            return string.Empty;
        }

        internal static void CreateInitialFolders(int albumId, HttpServerUtility server)
        {
            if (!Directory.Exists(server.MapPath(Common.Constants.MainContentFolder)))
            {
                Directory.CreateDirectory(server.MapPath(Common.Constants.MainContentFolder));
            }

            if (!Directory.Exists(server.MapPath(Common.Constants.MainContentFolder + "\\" + albumId)))
            {
                Directory.CreateDirectory(server.MapPath(Common.Constants.MainContentFolder + "\\" + albumId));
            }

            if (
                !Directory.Exists(
                    server.MapPath(
                        Common.Constants.MainContentFolder + "\\" + albumId + "\\"
                        + Common.Constants.ImageFolderOriginal)))
            {
                Directory.CreateDirectory(
                    server.MapPath(
                        Common.Constants.MainContentFolder + "\\" + albumId + "\\"
                        + Common.Constants.ImageFolderOriginal));
            }

            if (
                !Directory.Exists(
                    server.MapPath(
                        Common.Constants.MainContentFolder + "\\" + albumId + "\\" + Common.Constants.ImageFolderMiddle)))
            {
                Directory.CreateDirectory(
                    server.MapPath(
                        Common.Constants.MainContentFolder + "\\" + albumId + "\\" + Common.Constants.ImageFolderMiddle));
            }

            if (
                !Directory.Exists(
                    server.MapPath(
                        Common.Constants.MainContentFolder + "\\" + albumId + "\\" + Common.Constants.ImageFolderLow)))
            {
                Directory.CreateDirectory(
                    server.MapPath(
                        Common.Constants.MainContentFolder + "\\" + albumId + "\\" + Common.Constants.ImageFolderLow));
            }
        }

        internal static void Save(Stream inputStream, ImageType type, string originalFilename, int albumId, HttpServerUtility server)
        {
            using (var fileStream = System.IO.File.Create(FileService.GetImageFolder(albumId, type, server) + originalFilename))
            {
                inputStream.Seek(0, SeekOrigin.Begin);
                inputStream.CopyTo(fileStream);
                fileStream.Close();
            }                         
        }
    }
}
