using System.Linq;
using ImageGallery.Web.Areas.Admin.Models.Image;

namespace ImageGallery.Web.Areas.Admin.Controllers
{
    using System.Web.Mvc;

    using ImageGallery.Services.Album;
    using ImageGallery.Services.Image;
    using ImageGallery.Services.User;
    using ImageGallery.Web.Areas.Admin.Models.Album;
    using ImageGallery.Web.Infrastructure.Mappings;

    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;

    public class JsonController : BaseController
    {
        private readonly IAlbumService albumService;

        private readonly IImageService imageService;

        public JsonController(IUserService userService, IAlbumService albumService, IImageService imageService)
            : base(userService)
        {
            this.albumService = albumService;
            this.imageService = imageService;
        }

        public ActionResult Albums_Read([DataSourceRequest] DataSourceRequest request)
        {
            var tasks = this.albumService.GetAll().To<AlbumListViewModel>();
            return this.Json(tasks.ToDataSourceResult(request));
        }

        public ActionResult ImagesGrid_Read([DataSourceRequest] DataSourceRequest request)
        {
            var id = int.Parse(this.Session["AlbumId"].ToString());
            var result = this.imageService.GetAll().Where(x => x.AlbumId == id).To<ImageDetailsViewModel>(); 
            return Json(result.ToDataSourceResult(request));
        }
    }
}