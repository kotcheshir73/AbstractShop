using System.Collections.Generic;
using System.ComponentModel;

namespace AbstractShopServiceDAL.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        [DisplayName("Название продукта")]
        public string ProductName { get; set; }

        [DisplayName("Цена")]
        public decimal Price { get; set; }

        public List<ProductComponentViewModel> ProductComponents { get; set; }
    }
}