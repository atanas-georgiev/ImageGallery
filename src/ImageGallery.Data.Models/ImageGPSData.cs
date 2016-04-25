using System.ComponentModel.DataAnnotations;
using ImageGallery.Data.Common;
using ImageGallery.Data.Common.Models;

namespace ImageGallery.Data.Models
{
    public class ImageGpsData : BaseModel<int>, IHavePrimaryKey<int>
    {
        [Required]
        [MaxLength(200)]
        public string LocationName { get; set; }

        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Longitude { get; set; }
    }
}
