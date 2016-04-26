using System;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using ImageGallery.Data.Models;
using ImageGallery.Web.Infrastructure.Mappings;

namespace ImageGallery.Web.Areas.Admin.Models.Image
{
    public class ImageDetailsViewModel : IMapFrom<Data.Models.Image>, IHaveCustomMappings
    {
        public int Id { get; set; }

        [MaxLength(200)]
        public string LocationName { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

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

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Data.Models.Image, ImageDetailsViewModel>(string.Empty)
                .ForMember(m => m.LocationName, opt => opt.MapFrom(c => c.ImageGpsData.LocationName))
                .ForMember(m => m.Latitude, opt => opt.MapFrom(c => c.ImageGpsData.Latitude))
                .ForMember(m => m.Longitude, opt => opt.MapFrom(c => c.ImageGpsData.Longitude));
        }
    }
}