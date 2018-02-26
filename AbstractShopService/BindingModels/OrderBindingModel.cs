using System.Runtime.Serialization;

namespace AbstractShopService.BindingModels
{
    [DataContract]
    public class OrderBindingModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int ClientId { get; set; }

        [DataMember]
        public int ProductId { get; set; }

        [DataMember]
        public int? ImplementerId { get; set; }

        [DataMember]
        public int Count { get; set; }

        [DataMember]
        public decimal Sum { get; set; }
    }
}
