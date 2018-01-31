using AbstractShopModel;
using AbstractShopService.BindingModels;
using AbstractShopService.Interfaces;
using AbstractShopService.ViewModels;
using System;
using System.Collections.Generic;

namespace AbstractShopService.ImplementationsList
{
    public class StockServiceList : IStockService
    {
        private DataListSingleton source;

        public StockServiceList()
        {
            source = DataListSingleton.GetInstance();
        }

        public List<StockViewModel> GetList()
        {
            List<StockViewModel> result = new List<StockViewModel>();
            for (int i = 0; i < source.Stocks.Count; ++i)
            {
                List<StockComponentViewModel> StockComponents = new List<StockComponentViewModel>();
                for (int j = 0; j < source.StockComponents.Count; ++j)
                {
                    if (source.StockComponents[j].StockId == source.Stocks[i].Id)
                    {
                        StockComponents.Add(new StockComponentViewModel
                        {
                            Id = source.StockComponents[j].Id,
                            StockId = source.StockComponents[j].StockId,
                            ComponentId = source.StockComponents[j].ComponentId,
                            Count = source.StockComponents[j].Count
                        });
                    }
                }
                result.Add(new StockViewModel
                {
                    Id = source.Stocks[i].Id,
                    StockName = source.Stocks[i].StockName,
                    StockComponents = StockComponents
                });
            }

            return result;
        }

        public StockViewModel GetElement(int id)
        {
            for (int i = 0; i < source.Stocks.Count; ++i)
            {
                List<StockComponentViewModel> StockComponents = new List<StockComponentViewModel>();
                for (int j = 0; j < source.StockComponents.Count; ++j)
                {
                    if (source.StockComponents[j].StockId == source.Stocks[i].Id)
                    {
                        StockComponents.Add(new StockComponentViewModel
                        {
                            Id = source.StockComponents[j].Id,
                            StockId = source.StockComponents[j].StockId,
                            ComponentId = source.StockComponents[j].ComponentId,
                            Count = source.StockComponents[j].Count
                        });
                    }
                }
                if (source.Stocks[i].Id == id)
                {
                    return new StockViewModel
                    {
                        Id = source.Stocks[i].Id,
                        StockName = source.Stocks[i].StockName,
                        StockComponents = StockComponents
                    };
                }
            }

            throw new Exception("Элемент не найден");
        }

        public void AddElement(StockBindingModel model)
        {
            int maxId = 0;
            for (int i = 0; i < source.Stocks.Count; ++i)
            {
                if (source.Stocks[i].Id > maxId)
                {
                    maxId = source.Stocks[i].Id;
                }
                if (source.Stocks[i].StockName == model.StockName)
                {
                    throw new Exception("Уже есть склад с таким названием");
                }
            }
            source.Stocks.Add(new Stock
            {
                Id = maxId + 1,
                StockName = model.StockName
            });
            int maxPCId = 0;
            for (int i = 0; i < source.StockComponents.Count; ++i)
            {
                if (source.StockComponents[i].Id > maxPCId)
                {
                    maxPCId = source.StockComponents[i].Id;
                }
            }
            for (int i = 0; i < model.StockComponents.Count; ++i)
            {
                source.StockComponents.Add(new StockComponent
                {
                    Id = ++maxPCId,
                    StockId = maxId + 1,
                    ComponentId = model.StockComponents[i].ComponentId,
                    Count = model.StockComponents[i].Count
                });
            }
        }

        public void UpdElement(StockBindingModel model)
        {
            int index = -1;
            for (int i = 0; i < source.Stocks.Count; ++i)
            {
                if (source.Stocks[i].Id == model.Id)
                {
                    index = i;
                }
                if (source.Stocks[i].StockName == model.StockName && 
                    source.Stocks[i].Id != model.Id)
                {
                    throw new Exception("Уже есть склад с таким названием");
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            source.Stocks[index].StockName = model.StockName;
            int maxPCId = 0;
            for (int i = 0; i < source.StockComponents.Count; ++i)
            {
                if (source.StockComponents[i].Id > maxPCId)
                {
                    maxPCId = source.StockComponents[i].Id;
                }
            }
            for (int i = 0; i < source.StockComponents.Count; ++i)
            {
                if (source.StockComponents[i].StockId == model.Id)
                {
                    bool flag = true;
                    for (int j = 0; j < model.StockComponents.Count; ++j)
                    {
                        if (source.StockComponents[i].Id == model.StockComponents[j].Id)
                        {
                            source.StockComponents[i].Count = model.StockComponents[j].Count;
                            flag = false;
                            break;
                        }
                    }
                    if (flag)
                    {
                        source.StockComponents.RemoveAt(i--);
                    }
                }
            }
            for (int i = 0; i < model.StockComponents.Count; ++i)
            {
                if (model.StockComponents[i].Id == 0)
                {
                    source.StockComponents.Add(new StockComponent
                    {
                        Id = ++maxPCId,
                        StockId = model.Id,
                        ComponentId = model.StockComponents[i].ComponentId,
                        Count = model.StockComponents[i].Count
                    });
                }
            }
        }

        public void DelElement(int id)
        {
            for (int i = 0; i < source.StockComponents.Count; ++i)
            {
                if (source.StockComponents[i].StockId == id)
                {
                    source.StockComponents.RemoveAt(i--);
                }
            }
            for (int i = 0; i < source.Stocks.Count; ++i)
            {
                if (source.Stocks[i].Id == id)
                {
                    source.Stocks.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
    }
}
