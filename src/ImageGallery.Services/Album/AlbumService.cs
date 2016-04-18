using System.Data.Entity;
using System.Linq;
using ImageGallery.Data.Common;

namespace ImageGallery.Services.Album
{
    public class AlbumService : IAlbumService
    {
        private IRepository<Data.Models.Album, int> albums;

        public AlbumService(IRepository<Data.Models.Album, int> albums)
        {
            this.albums = albums;
        }


        public IQueryable<Data.Models.Album> GetAll()
        {
            return this.albums.All();
        }
    }
}
