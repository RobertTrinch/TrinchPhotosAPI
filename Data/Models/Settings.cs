using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrinchPhotos_Web.Database.Models
{
    public class Settings
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Required]
        public int Id { get; set; } // thanks george and ben
        [Required]
        public required string Key { get; set; }
        [Required]
        public required string Value { get; set; }
    }
}
