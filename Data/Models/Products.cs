using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace TrinchPhotosAPI.Data.Models
{
    public class Products
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Category { get; set; }
        [Required, Description("This is a preview of the product")]
        public string DisplayImageURL { get; set; }
        [Required, Description("This is the product - what is being ordered")]
        public string ProductImageUrl { get; set; } // This is what is being ordered
        [Required]
        public string CreatorId { get; set; }
        [Required]
        public string StripePriceId { get; set; }
        [Required]
        public string StripeProductId { get; set; }
        public long Price { get; set; }

    }
}
