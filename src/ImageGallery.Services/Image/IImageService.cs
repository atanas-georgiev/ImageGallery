namespace ImageGallery.Services.Image
{
    using System.Linq;
    using System.Web;

    using ImageGallery.Data.Models;

    public interface IImageService
    {
        void Add(int albumId, HttpPostedFileBase file, HttpServerUtility server);

        IQueryable<Image> GetAll();

        Image GetById(int id);

        void Update(Image image);

        void Remove(int id);
    }
}