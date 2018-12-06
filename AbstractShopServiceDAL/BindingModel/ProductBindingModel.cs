﻿using System.Collections.Generic;

namespace AbstractShopServiceDAL.BindingModels
{
    public class ProductBindingModel
    {
        public int Id { get; set; }

        public string ProductName { get; set; }

        public decimal Price { get; set; }

        public List<ProductComponentBindingModel> ProductComponents { get; set; }
    }
}