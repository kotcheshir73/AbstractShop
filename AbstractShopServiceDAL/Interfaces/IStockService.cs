using AbstractShopServiceDAL.BindingModels;
using AbstractShopServiceDAL.ViewModels;
using System.Collections.Generic;

namespace AbstractShopServiceDAL.Interfaces
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