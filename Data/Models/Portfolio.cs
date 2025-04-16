using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TrinchPhotosAPI.Data.Models
{
    public class Portfolio
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Required]
        public int Id { get; set; } // thanks george and ben
        [Required]
        public string ImageLink { get; set; }
        [Required]
        public string ImageName { get; set; }
        [Required]
        public string ImageDescription { get; set; }

    }
}
