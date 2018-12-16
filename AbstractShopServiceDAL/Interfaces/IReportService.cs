using AbstractShopServiceDAL.BindingModel;
using AbstractShopServiceDAL.ViewModel;
using System.Collections.Generic;

namespace AbstractShopServiceDAL.Interfaces
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