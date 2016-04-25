using System;

namespace ImageGallery.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using ImageGallery.Data.Common;
    using ImageGallery.Data.Common.Models;

    public class Image : BaseModel<int>, IHavePrimaryKey<int>
    {
        public virtual Album Album { get; set; }

        public virtual int? AlbumId { get; set; }

        public virtual ImageGpsData ImageGpsData { get; set; }

        public virtual int? ImageGpsDataId { get; set; }

        [MaxLength(3000)]
        public string Description { get; set; }

        [MaxLength(100)]
        public string FileName { get; set; }

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

        public DateTime? DateTaken { get; set; }

        [MaxLength(50)]
        public string CameraModel { get; set; }

        [MaxLength(50)]
        public string CameraMaker { get; set; }

        public string Aperture { get; set; }

        public string ShutterSpeed { get; set; }

        public int? Iso { get; set; }

        public double? ExposureBiasStep { get; set; }

        public double? FocusLen { get; set; }
    }
}