using AbstractShopService.Attributies;
using AbstractShopService.BindingModels;
using AbstractShopService.ViewModels;
using System.Collections.Generic;

namespace AbstractShopService.Interfaces
{
    [CustomInterface("Интерфейс для работы с изделиями")]
    public interface IProductService
    {
        [CustomMethod("Метод получения списка изделий")]
        List<ProductViewModel> GetList();

        [CustomMethod("Метод получения изделия по id")]
        ProductViewModel GetElement(int id);

        [CustomMethod("Метод добавления изделия")]
        void AddElement(ProductBindingModel model);

        [CustomMethod("Метод изменения данных по изделию")]
        void UpdElement(ProductBindingModel model);

        [CustomMethod("Метод удаления изделия")]
        void DelElement(int id);
    }
}
