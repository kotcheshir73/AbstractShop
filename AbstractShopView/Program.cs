using AbstractShopService.ImplementationsList;
using AbstractShopService.Interfaces;
using System;
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
            currentContainer.RegisterType<IClientService, ClientServiceList>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IComponentService, ComponentServiceList>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IImplementerService, ImplementerServiceList>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IProductService, ProductServiceList>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IStockService, StockServiceList>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IMainService, MainServiceList>(new HierarchicalLifetimeManager());
            
            return currentContainer;
        }
    }
}
