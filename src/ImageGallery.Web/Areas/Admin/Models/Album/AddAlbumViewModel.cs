namespace ImageGallery.Web.Areas.Admin.Models.Album
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using ImageGallery.Data.Models;
    using ImageGallery.Web.Infrastructure.Mappings;

    public class AddAlbumViewModel : IMapFrom<Album>
    {
        [Required]
        [UIHint("DateTime")]
        public DateTime Date { get; set; }

        [MaxLength(3000)]
        [UIHint("Editor")]
        [AllowHtml]
        public string Description { get; set; }

        [Key]
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(150)]
        [UIHint("String")]
        public string Title { get; set; }
    }
}