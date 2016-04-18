using System;

namespace ImageGallery.Data.Common.Models
{
    public interface IDeletableEntity
    {
        DateTime? DeletedOn { get; set; }

        bool IsDeleted { get; set; }
    }
}