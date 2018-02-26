using System.Runtime.Serialization;

namespace AbstractShopService
{
    [DataContract]
    public enum InterfacesName
    {
        IClientService,

        IComponentService,

        IImplementerService,

        IProductService,

        IStockService,

        IMainService,

        IReportService
    }
}
