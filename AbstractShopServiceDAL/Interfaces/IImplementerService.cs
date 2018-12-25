using AbstractShopServiceDAL.Attributies;
using AbstractShopServiceDAL.BindingModels;
using AbstractShopServiceDAL.ViewModels;
using System.Collections.Generic;

namespace AbstractShopServiceDAL.Interfaces
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

        [CustomMethod("Метод получения наименне загруженного работника")]
        ImplementerViewModel GetFreeWorker();
    }
}