using AbstractShopService;
using AbstractShopService.ImplementationsBD;
using AbstractShopService.Interfaces;
using System;
using System.Configuration;
using System.Data.Entity;
using Unity;
using Unity.Lifetime;

namespace AbstractShopServer
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = BuildUnityContainer();
            TSPServer server = container.Resolve<TSPServer>();
            server.RunServer(ConfigurationManager.AppSettings["IPAddress"], Convert.ToInt32(ConfigurationManager.AppSettings["IPPort"]));
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var currentContainer = new UnityContainer();
            currentContainer.RegisterType<DbContext, AbstractDbContext>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IClientService, ClientServiceBD>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IComponentService, ComponentServiceBD>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IImplementerService, ImplementerServiceBD>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IProductService, ProductServiceBD>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IStockService, StockServiceBD>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IMainService, MainServiceBD>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IReportService, ReportServiceBD>(new HierarchicalLifetimeManager());

            return currentContainer;
        }
    }
}
