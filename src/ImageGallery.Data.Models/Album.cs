using System.ComponentModel.DataAnnotations;

namespace ImageGallery.Data.Models
{
    public class Album
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }
    }
}
