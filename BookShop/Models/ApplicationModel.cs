using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace BookShop.Models
{
    public class ApplicationModel
    {
        [Key]
        public int ApplicationId { get; set; }

        [Required]
        public int JobListingId { get; set; }

        [Required]
        public string? Message { get; set; }

        // Navigation property nếu cần
        public string? Description { get; set; }
        [DisplayName("Display Offers of Jobs")]
        [Range(1, 10)]
        public int DisplayOrder { get; set; }
    }
}
