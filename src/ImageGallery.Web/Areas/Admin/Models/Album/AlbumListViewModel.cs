using System;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using ImageGallery.Infrastructure.Mappings;

namespace ImageGallery.Areas.Admin.Models.Album
{
    public class AlbumListViewModel : IMapFrom<Data.Models.Album>, IHaveCustomMappings
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(150)]
        public string Title { get; set; }

        [MaxLength(3000)]
        public string Description { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            
        }
    }
}