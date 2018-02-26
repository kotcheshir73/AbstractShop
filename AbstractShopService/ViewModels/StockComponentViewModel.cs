using System.Runtime.Serialization;

namespace AbstractShopService.ViewModels
{
    [DataContract]
    public class StockComponentViewModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int StockId { get; set; }

        [DataMember]
        public int ComponentId { get; set; }

        [DataMember]
        public string ComponentName { get; set; }

        [DataMember]
        public int Count { get; set; }
    }
}
