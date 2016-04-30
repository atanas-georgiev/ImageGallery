namespace ImageGallery.Web.Models.Album
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;

    using ImageGallery.Data.Models;
    using ImageGallery.Web.Infrastructure.Mappings;
    using ImageGallery.Web.Models.Image;

    public class AlbumViewModel : IMapFrom<Album>, IHaveCustomMappings
    {
        public DateTime? EndDate { get; set; }

        public Guid Id { get; set; }

        public DateTime? StartDate { get; set; }

        public string Title { get; set; }

        public int ItemsCount { get; set; }

        public IEnumerable<string> Locations { get; set; }

        public ImageViewModel CoverImage { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Album, AlbumViewModel>(string.Empty)
                .ForMember(
                    m => m.StartDate,
                    opt => opt.MapFrom(c => c.Images.OrderBy(x => x.DateTaken).FirstOrDefault().DateTaken))
                .ForMember(
                    m => m.EndDate,
                    opt => opt.MapFrom(c => c.Images.OrderByDescending(x => x.DateTaken).FirstOrDefault().DateTaken))
                .ForMember(m => m.ItemsCount, opt => opt.MapFrom(c => c.Images.Count));
            //  .ForMember(m => m.CoverImage, opt => opt.MapFrom(c => c.Images.Where(x => x.Id == c.CoverId).To<>()));
        }
    }
}