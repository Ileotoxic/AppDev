using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BookShop.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        [DisplayName("Display Order of Category")]
        [Range(1,10)]
        public int DisplayOrder { get; set; }
    }
}
