using System.Collections.Generic;
using System.Runtime.Serialization;

namespace AbstractShopServiceDAL.ViewModels
{
    [DataContract]
    public class StockViewModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string StockName { get; set; }

        [DataMember]
        public List<StockComponentViewModel> StockComponents { get; set; }
    }
}