using System.Collections.Generic;
using System.Runtime.Serialization;

namespace AbstractShopService.ViewModels
{
    [DataContract]
    public class ResponseModel<T>
    {
        [DataMember]
        public bool Success { get; set; }

        [DataMember]
        public string ErrorMessage { get; set; }

        [DataMember]
        public T Response { get; set; }

        [DataMember]
        public List<T> ResponseList { get; set; }
    }
}
