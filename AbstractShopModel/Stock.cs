using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AbstractShopModel
{
    /// <summary>
    /// Хранилиище компонентов в магазине
    /// </summary>
    public class Stock
    {
        public int Id { get; set; }

        [Required]
        public string StockName { get; set; }

        [ForeignKey("StockId")]
        public virtual List<StockComponent> StockComponents { get; set; }
    }
}