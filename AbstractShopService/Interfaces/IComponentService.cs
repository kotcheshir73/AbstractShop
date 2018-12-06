using AbstractShopService.BindingModels;
using AbstractShopService.ViewModels;
using System.Collections.Generic;

namespace AbstractShopService.Interfaces
{
    public interface IComponentService
    {
        List<ComponentViewModel> GetList();

        ComponentViewModel GetElement(int id);

        void AddElement(ComponentBindingModel model);

        void UpdElement(ComponentBindingModel model);

        void DelElement(int id);
    }
}
