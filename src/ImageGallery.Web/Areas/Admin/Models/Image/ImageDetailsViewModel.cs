using System.ComponentModel.DataAnnotations;
using ImageGallery.Web.Infrastructure.Mappings;

namespace ImageGallery.Web.Areas.Admin.Models.Image
{
    public class ImageDetailsViewModel : IMapFrom<Data.Models.Image>
    {
        public int Id { get; set; }
        
        [MaxLength(3000)]
        public string Description { get; set; }

        [MaxLength(100)]
        public string FileName { get; set; }

        public int ImageIdentificator { get; set; }

        [MaxLength(100)]
        public string OriginalFileName { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(150)]
        public string Title { get; set; }
    }
}