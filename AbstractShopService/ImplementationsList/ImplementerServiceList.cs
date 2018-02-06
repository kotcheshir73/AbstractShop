using AbstractShopModel;
using AbstractShopService.BindingModels;
using AbstractShopService.Interfaces;
using AbstractShopService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

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
            List<ImplementerViewModel> result = source.Implementers
                .Select(rec => new ImplementerViewModel
                {
                    Id = rec.Id,
                    ImplementerFIO = rec.ImplementerFIO
                })
                .ToList();
            return result;
        }

        public ImplementerViewModel GetElement(int id)
        {
            Implementer element = source.Implementers.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new ImplementerViewModel
                {
                    Id = element.Id,
                    ImplementerFIO = element.ImplementerFIO
                };
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(ImplementerBindingModel model)
        {
            Implementer element = source.Implementers.FirstOrDefault(rec => rec.ImplementerFIO == model.ImplementerFIO);
            if (element != null)
            {
                throw new Exception("Уже есть сотрудник с таким ФИО");
            }
            int maxId = source.Implementers.Count > 0 ? source.Implementers.Max(rec => rec.Id) : 0;
            source.Implementers.Add(new Implementer
            {
                Id = maxId + 1,
                ImplementerFIO = model.ImplementerFIO
            });
        }

        public void UpdElement(ImplementerBindingModel model)
        {
            Implementer element = source.Implementers.FirstOrDefault(rec =>
                                        rec.ImplementerFIO == model.ImplementerFIO && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть сотрудник с таким ФИО");
            }
            element = source.Implementers.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.ImplementerFIO = model.ImplementerFIO;
        }

        public void DelElement(int id)
        {
            Implementer element = source.Implementers.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                source.Implementers.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }
}
