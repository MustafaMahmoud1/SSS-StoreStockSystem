using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSS_StoreStockSystem.DAL.Models
{
    public class Item : BaseModel
    {

        [MaxLength(7)]
        [Required]
        public string Code { get; set; }

        [MaxLength(100)]
        [Required]
        public string? Name { get; set; }

        public double Price { get; set; }
        public string? ImageName { get; set; }

        public ICollection<StoreItem> StoreItems { get; } = new HashSet<StoreItem>();
    }
}
