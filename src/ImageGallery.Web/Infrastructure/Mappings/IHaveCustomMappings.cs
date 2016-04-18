using AutoMapper;

namespace ImageGallery.Infrastructure.Mappings
{
    public interface IHaveCustomMappings
    {
        void CreateMappings(IConfiguration configuration);
    }
}