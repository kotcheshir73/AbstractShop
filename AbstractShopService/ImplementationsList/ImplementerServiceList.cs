using AbstractShopModel;
using AbstractShopService.BindingModels;
using AbstractShopService.Interfaces;
using AbstractShopService.ViewModels;
using System;
using System.Collections.Generic;

namespace AbstractShopService.ImplementationsList
{
    public class ImplementerServiceList : IImplementerService
    {
        private DataListSingleton source;

        public ImplementerServiceList()
        {
            source = DataListSingleton.GetInstance();
        }

        public List<ImplementerViewModel> GetList()
        {
            List<ImplementerViewModel> result = new List<ImplementerViewModel>();
            for (int i = 0; i < source.Implementers.Count; ++i)
            {
                result.Add(new ImplementerViewModel
                {
                    Id = source.Implementers[i].Id,
                    ImplementerFIO = source.Implementers[i].ImplementerFIO
                });
            }

            return result;
        }

        public ImplementerViewModel GetElement(int id)
        {
            for (int i = 0; i < source.Implementers.Count; ++i)
            {
                if (source.Implementers[i].Id == id)
                {
                    return new ImplementerViewModel
                    {
                        Id = source.Implementers[i].Id,
                        ImplementerFIO = source.Implementers[i].ImplementerFIO
                    };
                }
            }

            throw new Exception("Элемент не найден");
        }

        public void AddElement(ImplementerBindingModel model)
        {
            int maxId = 0;
            for (int i = 0; i < source.Implementers.Count; ++i)
            {
                if (source.Implementers[i].Id > maxId)
                {
                    maxId = source.Implementers[i].Id;
                }
                if (source.Implementers[i].ImplementerFIO == model.ImplementerFIO)
                {
                    throw new Exception("Уже есть сотрудник с таким ФИО");
                }
            }
            source.Implementers.Add(new Implementer
            {
                Id = maxId + 1,
                ImplementerFIO = model.ImplementerFIO
            });
        }

        public void UpdElement(ImplementerBindingModel model)
        {
            int index = -1;
            for (int i = 0; i < source.Implementers.Count; ++i)
            {
                if (source.Implementers[i].Id == model.Id)
                {
                    index = i;
                }
                if (source.Implementers[i].ImplementerFIO == model.ImplementerFIO && 
                    source.Implementers[i].Id != model.Id)
                {
                    throw new Exception("Уже есть сотрудник с таким ФИО");
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            source.Implementers[index].ImplementerFIO = model.ImplementerFIO;
        }

        public void DelElement(int id)
        {
            for (int i = 0; i < source.Implementers.Count; ++i)
            {
                if (source.Implementers[i].Id == id)
                {
                    source.Implementers.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
    }
}
