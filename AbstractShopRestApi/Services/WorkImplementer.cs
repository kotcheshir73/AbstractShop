using AbstractShopServiceDAL.BindingModels;
using AbstractShopServiceDAL.Interfaces;
using System;
using System.Threading;

namespace AbstractShopRestApi.Services
{
    public class WorkImplementer
    {
        private readonly IMainService _service;

        private readonly IImplementerService _serviceImplementer;

        private readonly int _implementerId;

        private readonly int _orderId;
        // семафор
        static Semaphore _sem = new Semaphore(3, 5);

        Thread myThread;

        public WorkImplementer(IMainService service, IImplementerService serviceImplementer, int implementerId, int orderId)
        {
            _service = service;
            _serviceImplementer = serviceImplementer;
            _implementerId = implementerId;
            _orderId = orderId;
            myThread = new Thread(Work);
            myThread.Start();
        }

        public void Work()
        {
            // забиваем мастерскую
            _sem.WaitOne();
            try
            {
                _service.TakeOrderInWork(new OrderBindingModel
                {
                    Id = _orderId,
                    ImplementerId = _implementerId
                });
                // Типа выполняем
                Thread.Sleep(1000);
                _service.FinishOrder(new OrderBindingModel
                {
                    Id = _orderId
                });
            }
            catch (Exception)
            {
                // делаем проброс
                throw;
            }
            finally
            {
                // освобождаем мастерскую
                _sem.Release();
            }
        }
    }
}