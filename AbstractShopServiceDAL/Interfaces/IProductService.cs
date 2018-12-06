using AbstractShopServiceDAL.BindingModels;
using AbstractShopServiceDAL.ViewModels;
using System.Collections.Generic;

namespace AbstractShopServiceDAL.Interfaces
{
    public interface IProductService
    {
        List<ProductViewModel> GetList();

        ProductViewModel GetElement(int id);

        void AddElement(ProductBindingModel model);

        void UpdElement(ProductBindingModel model);

        void DelElement(int id);
    }
}