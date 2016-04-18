using System.Linq;

namespace ImageGallery.Services.Album
{
    public interface IAlbumService
    {
        IQueryable<Data.Models.Album> GetAll();
    }
}
