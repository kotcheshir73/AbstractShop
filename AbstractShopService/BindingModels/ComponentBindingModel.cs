using System.Runtime.Serialization;

namespace AbstractShopService.BindingModels
{
    [DataContract]
    public class ComponentBindingModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string ComponentName { get; set; }
    }
}
