using System.Collections.Generic;
using System.Runtime.Serialization;

namespace AbstractShopServiceDAL.ViewModels
{
    [DataContract]
    public class ProductViewModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string ProductName { get; set; }

        [DataMember]
        public decimal Price { get; set; }

        [DataMember]
        public List<ProductComponentViewModel> ProductComponents { get; set; }
    }
}