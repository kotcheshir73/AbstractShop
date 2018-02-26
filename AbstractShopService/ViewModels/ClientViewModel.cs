using System.Runtime.Serialization;

namespace AbstractShopService.ViewModels
{
    [DataContract]
    public class ClientViewModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string ClientFIO { get; set; }
    }
}
