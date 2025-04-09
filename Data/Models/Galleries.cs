using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrinchPhotos_Web.Database.Models
{
    public class Galleries
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Required]
        public int GalleryId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string CreatorId { get; set; }
        [Required]
        public string DisplayImageUrl { get; set; }
        public string AltGalleryUrl { get; set; }

    }
}
