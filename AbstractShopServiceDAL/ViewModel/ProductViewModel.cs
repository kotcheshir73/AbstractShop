using System.Collections.Generic;

namespace AbstractShopServiceDAL.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        public string ProductName { get; set; }

        public decimal Price { get; set; }

        public List<ProductComponentViewModel> ProductComponents { get; set; }
    }
}