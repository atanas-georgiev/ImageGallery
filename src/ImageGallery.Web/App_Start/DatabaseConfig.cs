namespace ImageGallery.Web
{
    using System.Data.Entity;

    using ImageGallery.Data;
    using ImageGallery.Data.Migrations;

    public static class DatabaseConfig
    {
        public static void Initialize()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ImageGalleryDbContext, Configuration>());
        }
    }
}