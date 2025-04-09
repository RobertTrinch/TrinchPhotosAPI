using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrinchPhotos_Web.Database.Models
{
    public class Orders
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Required]
        public Guid OrderId { get; set; }
        [Required]
        public string CustomerName { get; set; }
        [Required]
        public string CustomerEmail { get; set; }
        [Required]
        public int WebViews { get; set; }
        [Required]
        public string FulfilledBy { get; set; }

    }
}
