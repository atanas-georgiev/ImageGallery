namespace ImageGallery.Web.Areas.Admin.Models.Album
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;

    using ImageGallery.Data.Models;
    using ImageGallery.Web.Infrastructure.Mappings;

    public class AlbumListViewModel : IMapFrom<Album>, IHaveCustomMappings
    {
        [Required]
        public DateTime Date { get; set; }

        [MaxLength(3000)]
        public string Description { get; set; }

        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(150)]
        public string Title { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
        }
    }
}