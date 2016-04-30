namespace ImageGallery.Services.Album
{
    using System;
    using System.Linq;

    using ImageGallery.Data.Common;
    using ImageGallery.Data.Models;

    public class AlbumService : IAlbumService
    {
        private IRepository<Album, Guid> albums;

        public AlbumService(IRepository<Album, Guid> albums)
        {
            this.albums = albums;
        }

        public void Add(Album album)
        {
            this.albums.Add(album);
        }

        public IQueryable<Album> GetAll()
        {
            return this.albums.All();
        }

        public Album GetById(Guid id)
        {
            return this.GetAll().FirstOrDefault(x => x.Id == id);
        }
    }
}