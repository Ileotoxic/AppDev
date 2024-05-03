using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

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

        public int CategoryId { get; set; }
        [ForeignKey(nameof(CategoryId))]
        [ValidateNever]
        public Category Category { get; set; }

        // Navigation property to reference the employer (IdentityUser)
        public ApplicationModel? Employer { get; set; }
        public string? Image { get; set; }
    }
}
