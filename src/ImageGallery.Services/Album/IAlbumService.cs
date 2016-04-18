using System.Linq;

namespace ImageGallery.Services.Album
{
    public interface IAlbumService
    {
        IQueryable<Data.Models.Album> GetAll();

        Data.Models.Album GetById(int id);

        void Add(Data.Models.Album album);
    }
}
