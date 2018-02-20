using System;
using System.Collections.Generic;

namespace AbstractShopService.ViewModels
{
    public class StocksLoadViewModel
    {
        public string StockName { get; set; }

        public int TotalCount { get; set; }

        public IEnumerable<Tuple<string, int>> Components { get; set; }
    }
}
