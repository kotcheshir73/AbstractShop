namespace AbstractShopServiceDAL.BindingModels
{
    public class StockComponentBindingModel
    {
        public int Id { get; set; }

        public int StockId { get; set; }

        public int ComponentId { get; set; }

        public int Count { get; set; }
    }
}