using AbstractShopModel;
using AbstractShopService.BindingModels;
using AbstractShopService.Interfaces;
using AbstractShopService.ViewModels;
using System;
using System.Collections.Generic;

namespace AbstractShopService.ImplementationsList
{
    public class MainServiceList : IMainService
    {
        private DataListSingleton source;

        public MainServiceList()
        {
            source = DataListSingleton.GetInstance();
        }

        public List<OrderViewModel> GetList()
        {
            List<OrderViewModel> result = new List<OrderViewModel>();
            for (int i = 0; i < source.Orders.Count; ++i)
            {
                string clientFIO = string.Empty;
                for (int j = 0; j < source.Clients.Count; ++j)
                {
                    if(source.Clients[j].Id == source.Orders[i].ClientId)
                    {
                        clientFIO = source.Clients[j].ClientFIO;
                        break;
                    }
                }
                string productName = string.Empty;
                for (int j = 0; j < source.Products.Count; ++j)
                {
                    if (source.Products[j].Id == source.Orders[i].ProductId)
                    {
                        productName = source.Products[j].ProductName;
                        break;
                    }
                }
                string implementerFIO = string.Empty;
                if(source.Orders[i].ImplementerId.HasValue)
                {
                    for (int j = 0; j < source.Implementers.Count; ++j)
                    {
                        if (source.Implementers[j].Id == source.Orders[i].ImplementerId.Value)
                        {
                            implementerFIO = source.Implementers[j].ImplementerFIO;
                            break;
                        }
                    }
                }
                result.Add(new OrderViewModel
                {
                    Id = source.Orders[i].Id,
                    ClientId = source.Orders[i].ClientId,
                    ClientFIO = clientFIO,
                    ProductId = source.Orders[i].ProductId,
                    ProductName = productName,
                    ImplementerId = source.Orders[i].ImplementerId,
                    ImplementerName = implementerFIO,
                    Count = source.Orders[i].Count,
                    Sum = source.Orders[i].Sum,
                    DateCreate = source.Orders[i].DateCreate.ToLongDateString(),
                    DateImplement = source.Orders[i].DateImplement?.ToLongDateString(),
                    Status = source.Orders[i].Status.ToString()
                });
            }
            return result;
        }

        public void CreateOrder(OrderBindingModel model)
        {
            int maxId = 0;
            for (int i = 0; i < source.Orders.Count; ++i)
            {
                if (source.Orders[i].Id > maxId)
                {
                    maxId = source.Clients[i].Id;
                }
            }
            source.Orders.Add(new Order
            {
                Id = maxId + 1,
                ClientId = model.ClientId,
                ProductId = model.ProductId,
                DateCreate = DateTime.Now,
                Count = model.Count,
                Sum = model.Sum,
                Status = OrderStatus.Принят
            });
        }

        public void TakeOrderInWork(OrderBindingModel model)
        {
            int index = -1;
            for (int i = 0; i < source.Orders.Count; ++i)
            {
                if (source.Orders[i].Id == model.Id)
                {
                    index = i;
                    break;
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            // смотрим по количеству компонентов на складах
            for(int i = 0; i < source.ProductComponents.Count; ++i)
            {
                if(source.ProductComponents[i].ProductId == source.Orders[index].ProductId)
                {
                    int countOnStocks = 0;
                    for(int j = 0; j < source.StockComponents.Count; ++j)
                    {
                        if(source.StockComponents[j].ComponentId == source.ProductComponents[i].ComponentId)
                        {
                            countOnStocks += source.StockComponents[j].Count;
                        }
                    }
                    if(countOnStocks < source.ProductComponents[i].Count * source.Orders[index].Count)
                    {
                        for (int j = 0; j < source.Components.Count; ++j)
                        {
                            if (source.Components[j].Id == source.ProductComponents[i].ComponentId)
                            {
                                throw new Exception("Не достаточно компонента " + source.Components[j].ComponentName + 
                                    " требуется " + source.ProductComponents[i].Count + ", в наличии " + countOnStocks);
                            }
                        }
                    }
                }
            }
            // списываем
            for (int i = 0; i < source.ProductComponents.Count; ++i)
            {
                if (source.ProductComponents[i].ProductId == source.Orders[index].ProductId)
                {
                    int countOnStocks = source.ProductComponents[i].Count * source.Orders[index].Count;
                    for (int j = 0; j < source.StockComponents.Count; ++j)
                    {
                        if (source.StockComponents[j].ComponentId == source.ProductComponents[i].ComponentId)
                        {
                            // компонентов на одном слкаде может не хватать
                            if (source.StockComponents[j].Count >= countOnStocks)
                            {
                                source.StockComponents[j].Count -= countOnStocks;
                                break;
                            }
                            else
                            {
                                countOnStocks -= source.StockComponents[j].Count;
                                source.StockComponents[j].Count = 0;
                            }
                        }
                    }
                }
            }
            source.Orders[index].ImplementerId = model.ImplementerId;
            source.Orders[index].DateImplement = DateTime.Now;
            source.Orders[index].Status = OrderStatus.Выполняется;
        }

        public void FinishOrder(int id)
        {
            int index = -1;
            for (int i = 0; i < source.Orders.Count; ++i)
            {
                if (source.Clients[i].Id == id)
                {
                    index = i;
                    break;
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            source.Orders[index].Status = OrderStatus.Готов;
        }

        public void PayOrder(int id)
        {
            int index = -1;
            for (int i = 0; i < source.Orders.Count; ++i)
            {
                if (source.Clients[i].Id == id)
                {
                    index = i;
                    break;
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            source.Orders[index].Status = OrderStatus.Оплачен;
        }

        public void PutComponentOnStock(StockComponentBindingModel model)
        {
            int maxId = 0;
            for (int i = 0; i < source.StockComponents.Count; ++i)
            {
                if(source.StockComponents[i].StockId == model.StockId && 
                    source.StockComponents[i].ComponentId == model.ComponentId)
                {
                    source.StockComponents[i].Count += model.Count;
                    return;
                }
                if (source.StockComponents[i].Id > maxId)
                {
                    maxId = source.StockComponents[i].Id;
                }
            }
            source.StockComponents.Add(new StockComponent
            {
                Id = ++maxId,
                StockId = model.StockId,
                ComponentId = model.ComponentId,
                Count = model.Count
            });
        }
    }
}
