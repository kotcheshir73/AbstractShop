using AbstractShopServiceDAL.Attributies;
using AbstractShopServiceDAL.BindingModels;
using AbstractShopServiceDAL.ViewModels;
using System.Collections.Generic;

namespace AbstractShopServiceDAL.Interfaces
{
    [CustomInterface("Интерфейс для работы с заказами")]
    public interface IMainService
    {
        [CustomMethod("Метод получения списка заказов")]
        List<OrderViewModel> GetList();

        [CustomMethod("Метод получения списка заказов для выполнения")]
        List<OrderViewModel> GetFreeOrders();

        [CustomMethod("Метод создания заказа")]
        void CreateOrder(OrderBindingModel model);

        [CustomMethod("Метод передачи заказа в работу")]
        void TakeOrderInWork(OrderBindingModel model);

        [CustomMethod("Метод передачи заказа на оплату")]
        void FinishOrder(OrderBindingModel model);

        [CustomMethod("Метод фиксирования оплаты по заказу")]
        void PayOrder(OrderBindingModel model);

        [CustomMethod("Метод пополнения компонент на складе")]
        void PutComponentOnStock(StockComponentBindingModel model);
    }
}