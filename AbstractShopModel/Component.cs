using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AbstractShopModel
{
    /// <summary>
    /// Компонент, требуемый для изготовления изделия
    /// </summary>
    public class Component
    {
        public int Id { get; set; }

        [Required]
        public string ComponentName { get; set; }

        [ForeignKey("ComponentId")]
        public virtual List<ProductComponent> ProductComponents { get; set; }

        [ForeignKey("ComponentId")]
        public virtual List<StockComponent> StockComponents { get; set; }
    }
}
