using System.Runtime.Serialization;

namespace AbstractShopServiceDAL.BindingModels
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