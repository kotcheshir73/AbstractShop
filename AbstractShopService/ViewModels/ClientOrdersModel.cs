using System.Runtime.Serialization;

namespace AbstractShopService.ViewModels
{
    [DataContract]
    public class ClientOrdersModel
    {
        [DataMember]
        public string ClientName { get; set; }

        [DataMember]
        public string DateCreate { get; set; }

        [DataMember]
        public string ProductName { get; set; }

        [DataMember]
        public int Count { get; set; }

        [DataMember]
        public decimal Sum { get; set; }

        [DataMember]
        public string Status { get; set; }
    }
}
