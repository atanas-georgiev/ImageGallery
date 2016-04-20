﻿namespace ImageGallery.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using ImageGallery.Data.Common;
    using ImageGallery.Data.Common.Models;

    public class Image : BaseModel<int>, IHavePrimaryKey<int>
    {
        public virtual Album Album { get; set; }

        public virtual int? AlbumId { get; set; }

        [MaxLength(3000)]
        public string Description { get; set; }

        [MaxLength(100)]
        public string FileName { get; set; }

        public int ImageIdentificator { get; set; }

        public int LowWidth { get; set; }

        public int LowHeight { get; set; }

        public int MidWidth { get; set; }

        public int MidHeight { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        [MaxLength(100)]
        public string OriginalFileName { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(150)]
        public string Title { get; set; }
    }
}