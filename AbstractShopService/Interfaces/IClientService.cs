using AbstractShopService.Attributies;
using AbstractShopService.BindingModels;
using AbstractShopService.ViewModels;
using System.Collections.Generic;

namespace AbstractShopService.Interfaces
{
    [CustomInterface("Интерфейс для работы с клиентами")]
    public interface IClientService
    {
        [CustomMethod("Метод получения списка клиентов")]
        List<ClientViewModel> GetList();

        [CustomMethod("Метод получения клиента по id")]
        ClientViewModel GetElement(int id);

        [CustomMethod("Метод добавления клиента")]
        void AddElement(ClientBindingModel model);

        [CustomMethod("Метод изменения данных по клиенту")]
        void UpdElement(ClientBindingModel model);

        [CustomMethod("Метод удаления клиента")]
        void DelElement(int id);
    }
}
