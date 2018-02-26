using System.Runtime.Serialization;

namespace AbstractShopService.BindingModels
{
    [DataContract]
    [KnownType(typeof(ClientBindingModel))]
    [KnownType(typeof(ComponentBindingModel))]
    [KnownType(typeof(ImplementerBindingModel))]
    [KnownType(typeof(OrderBindingModel))]
    [KnownType(typeof(ProductBindingModel))]
    [KnownType(typeof(ProductComponentBindingModel))]
    [KnownType(typeof(ReportBindingModel))]
    [KnownType(typeof(StockBindingModel))]
    [KnownType(typeof(StockComponentBindingModel))]
    public class RequestModel
    {
        [DataMember]
        public InterfacesName InterfaceName { get; set; }

        [DataMember]
        public MethodsName MethodName { get; set; }

        [DataMember]
        public object Request { get; set; }
    }
}
