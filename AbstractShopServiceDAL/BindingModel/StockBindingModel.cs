using System.Runtime.Serialization;

namespace AbstractShopServiceDAL.BindingModels
{
    [DataContract]
    public class StockBindingModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string StockName { get; set; }
    }
}