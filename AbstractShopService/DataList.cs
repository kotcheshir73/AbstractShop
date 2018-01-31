using AbstractShopModel;
using System.Collections.Generic;

namespace AbstractShopService
{
    class DataListSingleton
    {
        private static DataListSingleton instance;

        public List<Client> Clients { get; set; }

        public List<Component> Components { get; set; }

        public List<Implementer> Implementers { get; set; }

        public List<Order> Orders { get; set; }

        public List<Product> Products { get; set; }

        public List<ProductComponent> ProductComponents { get; set; }

        public List<Stock> Stocks { get; set; }

        public List<StockComponent> StockComponents { get; set; }

        private DataListSingleton()
        {
            Clients = new List<Client>();
            Components = new List<Component>();
            Implementers = new List<Implementer>();
            Orders = new List<Order>();
            Products = new List<Product>();
            ProductComponents = new List<ProductComponent>();
            Stocks = new List<Stock>();
            StockComponents = new List<StockComponent>();
        }

        public static DataListSingleton GetInstance()
        {
            if(instance == null)
            {
                instance = new DataListSingleton();
            }

            return instance;
        }
    }
}
