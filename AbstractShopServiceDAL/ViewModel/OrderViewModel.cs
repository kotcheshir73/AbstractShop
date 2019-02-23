using System.ComponentModel;

namespace AbstractShopServiceDAL.ViewModels
{
    public class OrderViewModel
    {
        public int Id { get; set; }

        public int ClientId { get; set; }

        [DisplayName("ФИО Клиента")]
        public string ClientFIO { get; set; }

        public int ProductId { get; set; }

        [DisplayName("Продукт")]
        public string ProductName { get; set; }

        [DisplayName("Количество")]
        public int Count { get; set; }

        [DisplayName("Сумма")]
        public decimal Sum { get; set; }

        [DisplayName("Статус")]
        public string Status { get; set; }

        [DisplayName("Дата создания")]
        public string DateCreate { get; set; }

        [DisplayName("Дата выполнения")]
        public string DateImplement { get; set; }
    }
}