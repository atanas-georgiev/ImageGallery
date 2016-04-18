using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ImageGallery.Areas.Admin.Models.Album;
using ImageGallery.Infrastructure.Mappings;
using ImageGallery.Services.Album;
using ImageGallery.Services.User;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Planex.Web.Areas.Manager.Controllers;

namespace ImageGallery.Areas.Admin.Controllers
{
    public class JsonController : BaseController
    {
        private readonly IAlbumService albumService;

        public JsonController(IUserService userService, IAlbumService albumService) : base(userService)
        {
            this.albumService = albumService;
        }

        public ActionResult ReadAllAlbums([DataSourceRequest] DataSourceRequest request)
        {
            var tasks = this.albumService.GetAll().To<AlbumListViewModel>();
            return this.Json(tasks.ToDataSourceResult(request));
        }
    }
}