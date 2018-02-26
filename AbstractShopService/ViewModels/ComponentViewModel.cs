using System.Runtime.Serialization;

namespace AbstractShopService.ViewModels
{
    [DataContract]
    public class ComponentViewModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string ComponentName { get; set; }
    }
}
