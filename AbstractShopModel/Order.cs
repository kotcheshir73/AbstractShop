using System;

namespace AbstractShopModel
{
    /// <summary>
    /// Заказ клиента
    /// </summary>
    public class Order
    {
        public int Id { get; set; }

        public int ClientId { get; set; }

        public int ProductId { get; set; }

        public int? ImplementerId { get; set; }

        public int Count { get; set; }

        public decimal Sum { get; set; }

        public OrderStatus Status { get; set; }

        public DateTime DateCreate { get; set; }

        public DateTime? DateImplement { get; set; }

        public virtual Client Client { get; set; }

        public virtual Product Product { get; set; }

        public virtual Implementer Implementer { get; set; }
    }
}
