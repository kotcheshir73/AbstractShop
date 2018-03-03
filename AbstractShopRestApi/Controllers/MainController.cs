using AbstractShopService.BindingModels;
using AbstractShopService.Interfaces;
using System;
using System.Web.Http;

namespace AbstractShopRestApi.Controllers
{
    public class MainController : ApiController
    {
        private readonly IMainService _service;

        public MainController(IMainService service)
        {
            _service = service;
        }

        [HttpGet]
        public IHttpActionResult GetList()
        {
            var list = _service.GetList();
            if (list == null)
            {
                InternalServerError(new Exception("Нет данных"));
            }
            return Ok(list);
        }

        [HttpPost]
        public void CreateOrder(OrderBindingModel model)
        {
            _service.CreateOrder(model);
        }

        [HttpPost]
        public void TakeOrderInWork(OrderBindingModel model)
        {
            _service.TakeOrderInWork(model);
        }

        [HttpPost]
        public void FinishOrder(OrderBindingModel model)
        {
            _service.FinishOrder(model.Id);
        }

        [HttpPost]
        public void PayOrder(OrderBindingModel model)
        {
            _service.PayOrder(model.Id);
        }

        [HttpPost]
        public void PutComponentOnStock(StockComponentBindingModel model)
        {
            _service.PutComponentOnStock(model);
        }
    }
}
