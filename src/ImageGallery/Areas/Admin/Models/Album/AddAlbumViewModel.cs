using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ImageGallery.Infrastructure.Mappings;

namespace ImageGallery.Areas.Admin.Models.Album
{
    public class AddAlbumViewModel : IMapFrom<Data.Models.Album>
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }        

        [Required]
        [MinLength(3)]
        [MaxLength(150)]
        [UIHint("String")]
        public string Title { get; set; }

        [MaxLength(3000)]
        [UIHint("Editor")]
        [AllowHtml]        
        public string Description { get; set; }

        [Required]
        [UIHint("DateTime")]
        public DateTime Date { get; set; }
    }
}