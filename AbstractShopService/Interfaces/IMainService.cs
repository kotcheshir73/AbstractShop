using AbstractShopService.Attributies;
using AbstractShopService.BindingModels;
using AbstractShopService.ViewModels;
using System.Collections.Generic;

namespace AbstractShopService.Interfaces
{
    [CustomInterface("Интерфейс для работы с заказами")]
    public interface IMainService
    {
        [CustomMethod("Метод получения списка заказов")]
        List<OrderViewModel> GetList();

        [CustomMethod("Метод создания заказа")]
        void CreateOrder(OrderBindingModel model);

        [CustomMethod("Метод передачи заказа в работу")]
        void TakeOrderInWork(OrderBindingModel model);

        [CustomMethod("Метод передачи заказа на оплату")]
        void FinishOrder(int id);

        [CustomMethod("Метод фиксирования оплаты по заказу")]
        void PayOrder(int id);

        [CustomMethod("Метод пополнения компонент на складе")]
        void PutComponentOnStock(StockComponentBindingModel model);
    }
}
