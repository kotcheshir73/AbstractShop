using AbstractShopService.Attributies;
using AbstractShopService.BindingModels;
using AbstractShopService.ViewModels;
using System.Collections.Generic;

namespace AbstractShopService.Interfaces
{
    [CustomInterface("Интерфейс для работы с отчетами")]
    public interface IReportService
    {
        [CustomMethod("Метод сохранения списка изделий в doc-файл")]
        void SaveProductPrice(ReportBindingModel model);

        [CustomMethod("Метод получения списка складов с количество компонент на них")]
        List<StocksLoadViewModel> GetStocksLoad();

        [CustomMethod("Метод сохранения списка списка складов с количество компонент на них в xls-файл")]
        void SaveStocksLoad(ReportBindingModel model);

        [CustomMethod("Метод получения списка заказов клиентов")]
        List<ClientOrdersModel> GetClientOrders(ReportBindingModel model);

        [CustomMethod("Метод сохранения списка заказов клиентов в pdf-файл")]
        void SaveClientOrders(ReportBindingModel model);
    }
}
