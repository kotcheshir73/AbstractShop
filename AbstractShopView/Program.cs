using AbstractShopService;
using AbstractShopService.ImplementationsBD;
using AbstractShopService.ImplementationsList;
using AbstractShopService.Interfaces;
using System;
using System.Data.Entity;
using System.Windows.Forms;
using Unity;
using Unity.Lifetime;

namespace AbstractShopView
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var container = BuildUnityContainer();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(container.Resolve<FormMain>());
        }

        public static IUnityContainer BuildUnityContainer()
        {
            var currentContainer = new UnityContainer();
            currentContainer.RegisterType<DbContext, AbstractDbContext>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IClientService, ClientServiceBD>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IComponentService, ComponentServiceBD>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IImplementerService, ImplementerServiceBD>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IProductService, ProductServiceBD>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IStockService, StockServiceBD>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IMainService, MainServiceBD>(new HierarchicalLifetimeManager());
            
            return currentContainer;
        }
    }
}
