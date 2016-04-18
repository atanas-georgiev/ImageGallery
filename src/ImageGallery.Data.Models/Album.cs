using System;
using System.ComponentModel.DataAnnotations;
using ImageGallery.Data.Common;
using ImageGallery.Data.Common.Models;

namespace ImageGallery.Data.Models
{
    public class Album : BaseModel<int>, IHavePrimaryKey<int>
    {
        [Required]
        [MinLength(3)]
        [MaxLength(150)]
        public string Title { get; set; }

        [MaxLength(3000)]
        public string Description { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }
}
