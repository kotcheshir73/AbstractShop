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
        static Semaphore _sem = new Semaphore(3, 3);

        Thread myThread;

        public WorkImplementer(IMainService service, IImplementerService serviceImplementer, int implementerId, int orderId)
        {
            _service = service;
            _serviceImplementer = serviceImplementer;
            _implementerId = implementerId;
            _orderId = orderId;
            try
            {
                _service.TakeOrderInWork(new OrderBindingModel
                {
                    Id = _orderId,
                    ImplementerId = _implementerId
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            myThread = new Thread(Work);
            myThread.Start();
        }

        public void Work()
        {
            try
            {
                // забиваем мастерскую
                _sem.WaitOne();
                // Типа выполняем
                Thread.Sleep(10000);
                _service.FinishOrder(new OrderBindingModel
                {
                    Id = _orderId
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                // освобождаем мастерскую
                _sem.Release();
            }
        }
    }
}