namespace AbstractShopService.BindingModels
{
    public class OrderBindingModel
    {
        public int Id { get; set; }

        public int ClientId { get; set; }

        public int ProductId { get; set; }

        public int Count { get; set; }

        public decimal Sum { get; set; }
    }
}
