﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ImageGallery.Areas.Admin.Models.Album;
using ImageGallery.Data.Models;
using ImageGallery.Infrastructure.Mappings;
using ImageGallery.Services.Album;
using ImageGallery.Services.User;
using Planex.Web.Areas.Manager.Controllers;

namespace ImageGallery.Areas.Admin.Controllers
{
    public class AlbumController : BaseController
    {
        private readonly IAlbumService albumService;

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add()
        {
            return this.View(new AddAlbumViewModel() { Date = DateTime.Today, Title = string.Empty });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(AddAlbumViewModel model)
        {
            if (this.ModelState.IsValid && model != null)
            {
                var album = new Album()
                {
                    Title = model.Title,
                    Description = model.Description,
                    Date = model.Date,
                    CreatedOn = DateTime.UtcNow
                };

                this.albumService.Add(album);                
                return this.RedirectToAction("Index");
            }

            return this.View(model);
        }

        public ActionResult Details(string id)
        {
            this.Session["AlbumId"] = id;
            var intId = int.Parse(id);
            var result =
                this.albumService.GetAll().Where(x => x.Id == intId).To<AlbumDetailsViewModel>().FirstOrDefault();
            return this.View(result);
        }

        public AlbumController(IUserService userService, IAlbumService albumService) : base(userService)
        {
            this.albumService = albumService;
        }
    }
}