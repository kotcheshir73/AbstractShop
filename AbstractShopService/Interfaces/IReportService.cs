using AbstractShopModel;
using AbstractShopService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractShopService.Interfaces
{
    public interface IReportService
    {
        void SaveProductPrice(string fileName);

        List<StocksLoadViewModel> GetStocksLoad();

        void SaveStocksLoad(string fileName);
    }
}
