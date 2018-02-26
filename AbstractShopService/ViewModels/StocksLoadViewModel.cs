using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace AbstractShopService.ViewModels
{
    [DataContract]
    public class StocksLoadViewModel
    {
        [DataMember]
        public string StockName { get; set; }

        [DataMember]
        public int TotalCount { get; set; }

        [DataMember]
        public List<StocksComponentLoadViewModel> Components { get; set; }
    }

    [DataContract]
    public class StocksComponentLoadViewModel
    {
        [DataMember]
        public string ComponentName { get; set; }

        [DataMember]
        public int Count { get; set; }
    }
}
