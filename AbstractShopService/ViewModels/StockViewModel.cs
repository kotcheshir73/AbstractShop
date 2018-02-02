﻿using System.Collections.Generic;

namespace AbstractShopService.ViewModels
{
    public class StockViewModel
    {
        public int Id { get; set; }

        public string StockName { get; set; }

        public List<StockComponentViewModel> StockComponents { get; set; }
    }
}
