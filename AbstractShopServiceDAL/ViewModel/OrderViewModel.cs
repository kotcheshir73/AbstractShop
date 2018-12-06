namespace AbstractShopServiceDAL.ViewModels
{
    public class OrderViewModel
    {
        public int Id { get; set; }

        public int ClientId { get; set; }

        public string ClientFIO { get; set; }

        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public int Count { get; set; }

        public decimal Sum { get; set; }

        public string Status { get; set; }

        public string DateCreate { get; set; }

        public string DateImplement { get; set; }
    }
}