using AbstractShopModel;
using AbstractShopServiceDAL.BindingModels;
using AbstractShopServiceDAL.Interfaces;
using AbstractShopServiceDAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AbstractShopServiceImplementList.ImplementationsList
{
    public class ComponentServiceList : IComponentService
    {
        private DataListSingleton source;

        public ComponentServiceList()
        {
            source = DataListSingleton.GetInstance();
        }

        public List<ComponentViewModel> GetList()
        {
            List<ComponentViewModel> result = source.Components.Select(rec => new ComponentViewModel
                {
                    Id = rec.Id,
                    ComponentName = rec.ComponentName
                })
                .ToList();
            return result;
        }

        public ComponentViewModel GetElement(int id)
        {
            Component element = source.Components.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new ComponentViewModel
                {
                    Id = element.Id,
                    ComponentName = element.ComponentName
                };
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(ComponentBindingModel model)
        {
            Component element = source.Components.FirstOrDefault(rec => rec.ComponentName == model.ComponentName);
            if (element != null)
            {
                throw new Exception("Уже есть компонент с таким названием");
            }
            int maxId = source.Components.Count > 0 ? source.Components.Max(rec => rec.Id) : 0;
            source.Components.Add(new Component
            {
                Id = maxId + 1,
                ComponentName = model.ComponentName
            });
        }

        public void UpdElement(ComponentBindingModel model)
        {
            Component element = source.Components.FirstOrDefault(rec => rec.ComponentName == model.ComponentName && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть компонент с таким названием");
            }
            element = source.Components.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.ComponentName = model.ComponentName;
        }

        public void DelElement(int id)
        {
            Component element = source.Components.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                source.Components.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }
}