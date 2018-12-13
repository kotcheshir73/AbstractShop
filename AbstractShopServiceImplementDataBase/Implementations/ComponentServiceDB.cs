using AbstractShopModel;
using AbstractShopServiceDAL.BindingModels;
using AbstractShopServiceDAL.Interfaces;
using AbstractShopServiceDAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AbstractShopServiceImplementDataBase.Implementations
{
    public class ComponentServiceDB : IComponentService
    {
        private AbstractDbContext context;

        public ComponentServiceDB(AbstractDbContext context)
        {
            this.context = context;
        }

        public List<ComponentViewModel> GetList()
        {
            List<ComponentViewModel> result = context.Components.Select(rec => new ComponentViewModel
                {
                    Id = rec.Id,
                    ComponentName = rec.ComponentName
                })
                .ToList();
            return result;
        }

        public ComponentViewModel GetElement(int id)
        {
            Component element = context.Components.FirstOrDefault(rec => rec.Id == id);
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
            Component element = context.Components.FirstOrDefault(rec => rec.ComponentName == model.ComponentName);
            if (element != null)
            {
                throw new Exception("Уже есть компонент с таким названием");
            }
            context.Components.Add(new Component
            {
                ComponentName = model.ComponentName
            });
            context.SaveChanges();
        }

        public void UpdElement(ComponentBindingModel model)
        {
            Component element = context.Components.FirstOrDefault(rec => rec.ComponentName == model.ComponentName && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть компонент с таким названием");
            }
            element = context.Components.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.ComponentName = model.ComponentName;
            context.SaveChanges();
        }

        public void DelElement(int id)
        {
            Component element = context.Components.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                context.Components.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }
}