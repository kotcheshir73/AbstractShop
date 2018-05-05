using AbstractShopService.Attributies;
using AbstractShopService.BindingModels;
using AbstractShopService.ViewModels;
using System.Collections.Generic;

namespace AbstractShopService.Interfaces
{
    [CustomInterface("Интерфейс для работы с компонентами")]
    public interface IComponentService
    {
        [CustomMethod("Метод получения списка компонент")]
        List<ComponentViewModel> GetList();

        [CustomMethod("Метод получения компонента по id")]
        ComponentViewModel GetElement(int id);

        [CustomMethod("Метод добавления компонента")]
        void AddElement(ComponentBindingModel model);

        [CustomMethod("Метод изменения данных по компоненту")]
        void UpdElement(ComponentBindingModel model);

        [CustomMethod("Метод удаления компонента")]
        void DelElement(int id);
    }
}
