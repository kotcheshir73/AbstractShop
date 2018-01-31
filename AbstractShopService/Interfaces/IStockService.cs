using AbstractShopService.BindingModels;
using AbstractShopService.ViewModels;
using System.Collections.Generic;

namespace AbstractShopService.Interfaces
{
    public interface IStockService
    {
        List<StockViewModel> GetList();

        StockViewModel GetElement(int id);

        void AddElement(StockBindingModel model);

        void UpdElement(StockBindingModel model);

        void DelElement(int id);
    }
}
