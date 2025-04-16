using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrinchPhotosAPI.Database.Models
{
    public class Orders
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Required]
        public Guid OrderId { get; set; }
        public string StripeSessionId { get; set; }
        [Required]
        public string CustomerName { get; set; }
        [Required]
        public string CustomerEmail { get; set; }
        public bool IsFulfilled { get; set; }
        public string FulfilledBy { get; set; }
        public string FulfillComment { get; set; }
        [Required]
        public int WebViews { get; set; }
        public Dictionary<string, string> Content { get; set; } = new Dictionary<string, string>();

    }
}
