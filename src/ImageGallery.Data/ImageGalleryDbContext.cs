namespace ImageGallery.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    using ImageGallery.Data.Common.Models;
    using ImageGallery.Data.Models;

    using Microsoft.AspNet.Identity.EntityFramework;

    public class ImageGalleryDbContext : IdentityDbContext<User>
    {
        public ImageGalleryDbContext()
            : base("ImageGalleryDb", false)
        {
        }

        public virtual IDbSet<Album> Albums { get; set; }

        public virtual IDbSet<ImageGpsData> ImageGpsDatas { get; set; }

        public virtual IDbSet<Image> Images { get; set; }

        public static ImageGalleryDbContext Create()
        {
            return new ImageGalleryDbContext();
        }

        public override int SaveChanges()
        {
            this.ApplyAuditInfoRules();
            return base.SaveChanges();
        }

        private void ApplyAuditInfoRules()
        {
            // Approach via @julielerman: http://bit.ly/123661P
            foreach (var entry in
                this.ChangeTracker.Entries()
                    .Where(
                        e =>
                        e.Entity is IAuditInfo && ((e.State == EntityState.Added) || (e.State == EntityState.Modified)))
                )
            {
                var entity = (IAuditInfo)entry.Entity;
                if (entry.State == EntityState.Added && entity.CreatedOn == default(DateTime))
                {
                    entity.CreatedOn = DateTime.Now;
                }
                else
                {
                    entity.ModifiedOn = DateTime.Now;
                }
            }
        }
    }
}