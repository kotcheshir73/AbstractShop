using System.Collections.Generic;
using System.Runtime.Serialization;

namespace AbstractShopServiceDAL.BindingModels
{
    [DataContract]
    public class ProductBindingModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string ProductName { get; set; }

        [DataMember]
        public decimal Price { get; set; }

        [DataMember]
        public List<ProductComponentBindingModel> ProductComponents { get; set; }
    }
}