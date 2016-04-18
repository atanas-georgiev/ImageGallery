namespace ImageGallery.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using ImageGallery.Data.Common;
    using ImageGallery.Data.Common.Models;

    public class Album : BaseModel<int>, IHavePrimaryKey<int>
    {
        private ICollection<Image> images;

        public Album()
        {
            this.images = new HashSet<Image>();
        }

        [Required]
        public DateTime Date { get; set; }

        [MaxLength(3000)]
        public string Description { get; set; }

        public virtual ICollection<Image> Images
        {
            get
            {
                return this.images;
            }

            set
            {
                this.images = value;
            }
        }

        [Required]
        [MinLength(3)]
        [MaxLength(150)]
        public string Title { get; set; }
    }
}