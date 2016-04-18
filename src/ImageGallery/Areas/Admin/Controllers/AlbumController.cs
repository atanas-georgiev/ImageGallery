using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ImageGallery.Areas.Admin.Models.Album;
using ImageGallery.Services.User;
using Planex.Web.Areas.Manager.Controllers;

namespace ImageGallery.Areas.Admin.Controllers
{
    public class AlbumController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add()
        {
            return this.View(new AddAlbumViewModel() { Date = DateTime.Today, Title = string.Empty });
        }

        public AlbumController(IUserService userService) : base(userService)
        {
        }
    }
}