using AbstractShopServiceDAL.BindingModels;
using AbstractShopServiceDAL.ViewModels;
using System.Collections.Generic;

namespace AbstractShopServiceDAL.Interfaces
{
    public interface IMainService
    {
        List<OrderViewModel> GetList();

        List<OrderViewModel> GetFreeOrders();

        void CreateOrder(OrderBindingModel model);

        void TakeOrderInWork(OrderBindingModel model);

        void FinishOrder(OrderBindingModel model);

        void PayOrder(OrderBindingModel model);

        void PutComponentOnStock(StockComponentBindingModel model);
    }
}