namespace ImageGallery.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using ImageGallery.Data.Common;
    using ImageGallery.Data.Common.Models;

    public class ImageGpsData : BaseModel<Guid>, IHavePrimaryKey<Guid>
    {
        [Required]
        public double Latitude { get; set; }

        [Required]
        [MaxLength(200)]
        public string LocationName { get; set; }

        [Required]
        public double Longitude { get; set; }
    }
}