﻿using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(ImageGallery.Web.Startup))]
namespace ImageGallery.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
