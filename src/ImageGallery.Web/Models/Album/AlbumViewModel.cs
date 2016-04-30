using System;
using System.Collections.Generic;
using System.Linq;
using ImageGallery.Web.Infrastructure.Mappings;

namespace ImageGallery.Web.Models.Album
{
    public class AlbumViewModel : IMapFrom<Data.Models.Album>
    {
        public Guid Id { get; set; }

        public DateTime Date { get; set; }        

        public string Description { get; set; }
        
        public string Title { get; set; }
    }
}