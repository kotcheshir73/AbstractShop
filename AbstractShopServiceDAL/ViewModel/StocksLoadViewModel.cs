using System;
using System.Collections.Generic;

namespace AbstractShopServiceDAL.ViewModel
{
    public class StocksLoadViewModel
    {
        public string StockName { get; set; }

        public int TotalCount { get; set; }

        public IEnumerable<Tuple<string, int>> Components { get; set; }
    }
}