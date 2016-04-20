using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ImageGallery.Services.Image;
using ImageGallery.Web.Infrastructure.Mappings;
using ImageGallery.Web.Models.Image;

namespace ImageGallery.Web.Controllers
{
    public class ImageController : Controller
    {
        private readonly IImageService imageService;

        public ImageController(IImageService imageService)
        {
            this.imageService = imageService;
        }

        // GET: Image
        public ActionResult Index()
        {
            var images = this.imageService.GetAll().To<ImageViewModel>().ToList();
            foreach (var image in images)
            {
                image.src = VirtualPathUtility.ToAbsolute(image.src);
            }
            return View(images);
        }
    }
}