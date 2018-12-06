using AbstractShopService.BindingModels;
using AbstractShopService.ViewModels;
using System.Collections.Generic;

namespace AbstractShopService.Interfaces
{
    public interface IMainService
    {
        List<OrderViewModel> GetList();

        void CreateOrder(OrderBindingModel model);

        void TakeOrderInWork(int id);

        void FinishOrder(int id);

        void PayOrder(int id);
    }
}
