using System.Runtime.Serialization;

namespace AbstractShopService.BindingModels
{
    [DataContract]
    public class StockComponentBindingModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int StockId { get; set; }

        [DataMember]
        public int ComponentId { get; set; }

        [DataMember]
        public int Count { get; set; }
    }
}
