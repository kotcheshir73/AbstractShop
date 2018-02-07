using AbstractShopModel;
using AbstractShopService.BindingModels;
using AbstractShopService.Interfaces;
using AbstractShopService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

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
            List<OrderViewModel> result = source.Orders
                .Select(rec => new OrderViewModel
                {
                    Id = rec.Id,
                    ClientId = rec.ClientId,
                    ProductId = rec.ProductId,
                    ImplementerId = rec.ImplementerId,
                    DateCreate = rec.DateCreate.ToLongDateString(),
                    DateImplement = rec.DateImplement?.ToLongDateString(),
                    Status = rec.Status.ToString(),
                    Count = rec.Count,
                    Sum = rec.Sum,
                    ClientFIO = source.Clients
                                    .FirstOrDefault(recC => recC.Id == rec.ClientId)?.ClientFIO,
                    ProductName = source.Products
                                    .FirstOrDefault(recP => recP.Id == rec.ProductId)?.ProductName,
                    ImplementerName = source.Implementers
                                    .FirstOrDefault(recI => recI.Id == rec.ImplementerId)?.ImplementerFIO
                })
                .ToList();
            return result;
        }

        public void CreateOrder(OrderBindingModel model)
        {
            int maxId = source.Orders.Count > 0 ? source.Orders.Max(rec => rec.Id) : 0;
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
            Order element = source.Orders.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            // смотрим по количеству компонентов на складах
            var productComponents = source.ProductComponents.Where(rec => rec.ProductId == element.ProductId);
            foreach(var productComponent in productComponents)
            {
                int countOnStocks = source.StockComponents
                                            .Where(rec => rec.ComponentId == productComponent.ComponentId)
                                            .Sum(rec => rec.Count);
                if (countOnStocks < productComponent.Count * element.Count)
                {
                    var componentName = source.Components
                                    .FirstOrDefault(rec => rec.Id == productComponent.ComponentId);
                    throw new Exception("Не достаточно компонента " + componentName?.ComponentName +
                        " требуется " + productComponent.Count + ", в наличии " + countOnStocks);
                }
            }
            // списываем
            foreach (var productComponent in productComponents)
            {
                int countOnStocks = productComponent.Count * element.Count;
                var stockComponents = source.StockComponents
                                            .Where(rec => rec.ComponentId == productComponent.ComponentId);
                foreach (var stockComponent in stockComponents)
                {
                    // компонентов на одном слкаде может не хватать
                    if (stockComponent.Count >= countOnStocks)
                    {
                        stockComponent.Count -= countOnStocks;
                        break;
                    }
                    else
                    {
                        countOnStocks -= stockComponent.Count;
                        stockComponent.Count = 0;
                    }
                }
            }
            element.ImplementerId = model.ImplementerId;
            element.DateImplement = DateTime.Now;
            element.Status = OrderStatus.Выполняется;
        }

        public void FinishOrder(int id)
        {
            Order element = source.Orders.FirstOrDefault(rec => rec.Id == id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.Status = OrderStatus.Готов;
        }

        public void PayOrder(int id)
        {
            Order element = source.Orders.FirstOrDefault(rec => rec.Id == id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.Status = OrderStatus.Оплачен;
        }

        public void PutComponentOnStock(StockComponentBindingModel model)
        {
            StockComponent element = source.StockComponents
                                                .FirstOrDefault(rec => rec.StockId == model.StockId && 
                                                                    rec.ComponentId == model.ComponentId);
            if(element != null)
            {
                element.Count += model.Count;
            }
            else
            {
                int maxId = source.StockComponents.Count > 0 ? source.StockComponents.Max(rec => rec.Id) : 0;
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
}
