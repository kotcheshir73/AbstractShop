using System.Runtime.Serialization;

namespace AbstractShopService.ViewModels
{
    [DataContract]
    public class ImplementerViewModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string ImplementerFIO { get; set; }
    }
}
