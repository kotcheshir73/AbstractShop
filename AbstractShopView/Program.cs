using AbstractShopServiceDAL.Interfaces;
using AbstractShopServiceImplementDataBase;
using AbstractShopServiceImplementDataBase.Implementations;
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
            currentContainer.RegisterType<IClientService, ClientServiceDB>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IComponentService, ComponentServiceDB>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IProductService, ProductServiceDB>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IStockService, StockServiceDB>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IMainService, MainServiceDB>(new HierarchicalLifetimeManager());

            currentContainer.RegisterType<IReportService, ReportServiceDB>(new HierarchicalLifetimeManager());
            return currentContainer;
        }
    }
}