using System.Runtime.Serialization;

namespace AbstractShopService.BindingModels
{
    [DataContract]
    public class ProductComponentBindingModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int ProductId { get; set; }

        [DataMember]
        public int ComponentId { get; set; }

        [DataMember]
        public int Count { get; set; }
    }
}
