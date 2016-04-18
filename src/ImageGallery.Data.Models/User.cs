namespace ImageGallery.Data.Models
{
    using System;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using ImageGallery.Data.Common;
    using ImageGallery.Data.Common.Models;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class User : IdentityUser, IHavePrimaryKey<string>, IAuditInfo, IDeletableEntity
    {
        public DateTime CreatedOn { get; set; }

        public DateTime? DeletedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }
    }
}