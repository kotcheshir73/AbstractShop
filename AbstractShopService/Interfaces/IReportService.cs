using AbstractShopService.BindingModels;
using AbstractShopService.ViewModels;
using System.Collections.Generic;

namespace AbstractShopService.Interfaces
{
    public interface IReportService
    {
        void SaveProductPrice(ReportBindingModel model);

        List<StocksLoadViewModel> GetStocksLoad();

        void SaveStocksLoad(ReportBindingModel model);

        List<ClientOrdersModel> GetClientOrders(ReportBindingModel model);

        void SaveClientOrders(ReportBindingModel model);
    }
}
