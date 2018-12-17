using System.Runtime.Serialization;

namespace AbstractShopServiceDAL.ViewModels
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