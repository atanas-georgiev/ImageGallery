using System;
using System.ComponentModel.DataAnnotations;
using ImageGallery.Data.Common;
using ImageGallery.Data.Common.Models;

namespace ImageGallery.Data.Models
{
    public class Image : BaseModel<int>, IHavePrimaryKey<int>
    {
        [Required]
        [MinLength(3)]
        [MaxLength(150)]
        public string Title { get; set; }

        [MaxLength(3000)]
        public string Description { get; set; }

        [MaxLength(100)]
        public string OriginalFileName { get; set; }

        public int ImageIdentificator { get; set; }

        [MaxLength(100)]
        public string FileName { get; set; }

        public virtual int? AlbumId { get; set; }

        public virtual Album Album { get; set; }
    }
}
