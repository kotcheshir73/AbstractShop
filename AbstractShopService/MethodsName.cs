using System.Runtime.Serialization;

namespace AbstractShopService
{
    [DataContract]
    public enum MethodsName
    {
        GetList,

        GetElement,

        AddElement,

        UpdElement,

        DelElement,

        CreateOrder,

        TakeOrderInWork,

        FinishOrder,

        PayOrder,

        PutComponentOnStock,

        GetStocksLoad,

        GetClientOrders,

        SaveProductPrice,

        SaveStocksLoad,
        
        SaveClientOrders
    }
}
