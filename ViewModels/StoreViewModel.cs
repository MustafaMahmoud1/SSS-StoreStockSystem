using SSS_StoreStockSystem.DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace SSS_StoreStockSystem.ViewModels
{
    public class StoreViewModel
    {
        public int Id { get; set; }

        [MaxLength(7)]
        [MinLength(7)]
        [Required]
        [RegularExpression(@"^[A-Za-z]{3}-\d{3}$", ErrorMessage = "Code must be in the format of three letters, a hyphen, and three numbers.")]
        public string Code { get; set; }

        [MaxLength(100)]
        [MinLength(2)]
        [Required]
        public string Name { get; set; }

        [MaxLength(100)]
        [MinLength(2)]
        public string? Location { get; set; }

        public IFormFile Image { get; set; }
        public string ImageName { get; set; }

        public ICollection<StoreItem> StoreItems { get; } = new HashSet<StoreItem>();
    }
}
