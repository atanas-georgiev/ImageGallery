namespace ImageGallery.Services.Album
{
    using System;
    using System.Linq;

    using ImageGallery.Data.Models;

    public interface IAlbumService
    {
        void Add(Album album);

        IQueryable<Album> GetAll();

        Album GetById(Guid id);
    }
}