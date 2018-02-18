using AbstractShopService.BindingModels;
using AbstractShopService.ViewModels;
using System.Collections.Generic;

namespace AbstractShopService.Interfaces
{
    public interface IReportService
    {
        void SaveProductPrice(string fileName);

        List<StocksLoadViewModel> GetStocksLoad();

        void SaveStocksLoad(string fileName);

        List<ClientOrdersModel> GetClientOrders(ReportBindingModel model);

        void SaveClientOrders(string fileName, ReportBindingModel model);
    }
}
