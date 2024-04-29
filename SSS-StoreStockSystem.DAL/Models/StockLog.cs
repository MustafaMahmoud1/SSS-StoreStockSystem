using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSS_StoreStockSystem.DAL.Models
{
    public class StockLog
    {
        public int StoreId { get; set; }
        public int ItemId { get; set; }
        public string Machine { get; set; }
        public DateTime DateTime { get; set; }
        public Proccess Process { get; set; }
        public int OldQuantity { get; set; }
        public int NewQuantity { get; set; }


    }
    public enum Proccess
    {
        Purchase,
        Sell
    }
}
