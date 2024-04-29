using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookShop.Models
{
    public class JobListingModel
    {
        [Key]
        public int JobListingId { get; set; }
        [Required]
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime ApplicationDeadline { get; set; }
        [Required]
        public string? Location { get; set; }

        // Updated foreign key property
        [ForeignKey(nameof(ApplicationId))]
        public string? ApplicationId { get; set; }  // Change data type to string

        // Navigation property to reference the employer (IdentityUser)
        public ApplicationModel? Employer { get; set; }
        public string? Image { get; set; }
    }
}
