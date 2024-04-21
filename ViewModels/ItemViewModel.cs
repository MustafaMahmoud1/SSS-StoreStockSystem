using Newtonsoft.Json.Serialization;
using SSS_StoreStockSystem.DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace SSS_StoreStockSystem.ViewModels
{
    public class ItemViewModel
    {
        public int Id { get; set; }

        [MaxLength(7, ErrorMessage = "Code must be 7 characters.")]
        [MinLength(7, ErrorMessage = "Code must be 7 characters.")]
        [Required]
        [RegularExpression(@"^[A-Za-z]{3}-\d{3}$", ErrorMessage = "Code must be in the format of three letters, a hyphen, and three numbers.")]
        public string Code { get; set; }

        [MaxLength(100, ErrorMessage ="Name must be less than 100 characters.")]
        [MinLength(2, ErrorMessage = "Name must be more than 2 characters.")]
        [Required]
        [RegularExpression(@"^[A-Za-z]{2,100}$", ErrorMessage = "Name must be between 2 and 100 alphabetic characters.")]
        public string? Name { get; set; }

        [Range(0, 9999)]
        public double Price { get; set; }

        public IFormFile Image { get; set; }
        public string ImageName { get; set; }

        public ICollection<StoreItem> StoreItems { get; } = new HashSet<StoreItem>();
    }
}
