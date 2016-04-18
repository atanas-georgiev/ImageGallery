using System.Linq;
using System.Web;

namespace ImageGallery.Services.Image
{
    public interface IImageService
    {
        IQueryable<Data.Models.Image> GetAll();

        Data.Models.Image GetById(int id);

        void Add(int albumId, HttpPostedFileBase file, HttpServerUtility server);
    }
}
