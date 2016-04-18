using Microsoft.Owin;

[assembly: OwinStartup(typeof(ImageGallery.Web.Startup))]

namespace ImageGallery.Web
{
    using Owin;

    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            this.ConfigureAuth(app);
        }
    }
}