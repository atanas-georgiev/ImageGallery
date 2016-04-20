namespace ImageGallery.Web.Models.Image
{
    using AutoMapper;
    using ImageGallery.Web.Infrastructure.Mappings;

    public class ImageViewModel : IMapFrom<Data.Models.Image>, IHaveCustomMappings
    {
        public string tumbsrc { get; set; }

        public string msrc { get; set; }

        public string src { get; set; }

        public int w { get; set; }

        public int h { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Data.Models.Image, ImageViewModel>(string.Empty)
                .ForMember(m => m.src,
                    opt =>
                        opt.MapFrom(
                            c =>
                                Common.Constants.MainContentFolder + "\\" + c.AlbumId + "\\" +
                                Common.Constants.ImageFolderOriginal + "\\" + c.FileName))
                .ForMember(m => m.msrc,
                    opt =>
                        opt.MapFrom(
                            c =>
                                Common.Constants.MainContentFolder + "\\" + c.AlbumId + "\\" +
                                Common.Constants.ImageFolderMiddle + "\\" + c.FileName))
                .ForMember(m => m.tumbsrc,
                    opt =>
                        opt.MapFrom(
                            c =>
                                Common.Constants.MainContentFolder + "\\" + c.AlbumId + "\\" +
                                Common.Constants.ImageFolderLow + "\\" + c.FileName))
                .ForMember(m => m.w, opt => opt.MapFrom(c => c.Width))
                .ForMember(m => m.h, opt => opt.MapFrom(c => c.Height));
        }
    }
}