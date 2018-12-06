using AbstractShopModel;
using System.Collections.Generic;

namespace AbstractShopServiceImplementList
{
    class DataListSingleton
    {
        private static DataListSingleton instance;

        public List<Client> Clients { get; set; }

        public List<Component> Components { get; set; }

        public List<Order> Orders { get; set; }

        public List<Product> Products { get; set; }

        public List<ProductComponent> ProductComponents { get; set; }

        private DataListSingleton()
        {
            Clients = new List<Client>();
            Components = new List<Component>();
            Orders = new List<Order>();
            Products = new List<Product>();
            ProductComponents = new List<ProductComponent>();
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