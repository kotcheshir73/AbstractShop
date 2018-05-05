using AbstractShopService.Attributies;
using AbstractShopService.BindingModels;
using AbstractShopService.ViewModels;
using System.Collections.Generic;

namespace AbstractShopService.Interfaces
{
    [CustomInterface("Интерфейс для работы с работниками")]
    public interface IImplementerService
    {
        [CustomMethod("Метод получения списка работников")]
        List<ImplementerViewModel> GetList();

        [CustomMethod("Метод получения работника по id")]
        ImplementerViewModel GetElement(int id);

        [CustomMethod("Метод добавления работника")]
        void AddElement(ImplementerBindingModel model);

        [CustomMethod("Метод изменения данных по работнику")]
        void UpdElement(ImplementerBindingModel model);

        [CustomMethod("Метод удаления работника")]
        void DelElement(int id);
    }
}
