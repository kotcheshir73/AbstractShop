using AbstractShopServiceDAL.Attributies;
using AbstractShopServiceDAL.BindingModel;
using AbstractShopServiceDAL.ViewModel;
using System.Collections.Generic;

namespace AbstractShopServiceDAL.Interfaces
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