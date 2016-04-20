using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ImageGallery.Services.Album;
using ImageGallery.Web.Infrastructure.Mappings;
using ImageGallery.Web.Models.Album;
using ImageGallery.Web.Models.Image;

namespace ImageGallery.Web.Controllers
{
    public class AlbumsController : Controller
    {
        private readonly IAlbumService albumService;

        public AlbumsController(IAlbumService albumService)
        {
            this.albumService = albumService;
        }
        
        public ActionResult Index()
        {
            var albums = this.albumService.GetAll().To<AlbumViewModel>();            
            return View(albums);
        }

        public ActionResult Details(int id)
        {
            var images = this.albumService.GetById(id).Images.AsQueryable().To<ImageViewModel>().ToList();
            foreach (var image in images)
            {
                image.src = VirtualPathUtility.ToAbsolute(image.src);
                image.tumbsrc = VirtualPathUtility.ToAbsolute(image.tumbsrc);
               // image.msrc = VirtualPathUtility.ToAbsolute(image.msrc);
            }
            return View(images);
        }
    }
}