using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrinchPhotos_Web.Database.Models
{
    public class Creators
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Required]
        public int Id { get; set; }
        [Required]
        public string DisplayName { get; set; }
        [Required]
        public string ProfilePictureUrl { get; set; }
        [Required]
        public string ContactEmail { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
