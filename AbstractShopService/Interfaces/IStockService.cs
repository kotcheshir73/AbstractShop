using AbstractShopService.Attributies;
using AbstractShopService.BindingModels;
using AbstractShopService.ViewModels;
using System.Collections.Generic;

namespace AbstractShopService.Interfaces
{
    [CustomInterface("Интерфейс для работы со складами")]
    public interface IStockService
    {
        [CustomMethod("Метод получения списка складов")]
        List<StockViewModel> GetList();

        [CustomMethod("Метод получения склада по id")]
        StockViewModel GetElement(int id);

        [CustomMethod("Метод добавления склада")]
        void AddElement(StockBindingModel model);

        [CustomMethod("Метод изменения данных по складу")]
        void UpdElement(StockBindingModel model);

        [CustomMethod("Метод удаления склада")]
        void DelElement(int id);
    }
}
