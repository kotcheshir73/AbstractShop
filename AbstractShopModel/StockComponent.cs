namespace AbstractShopModel
{
    /// <summary>
    /// Сколько компонентов хранится на складе
    /// </summary>
    public class StockComponent
    {
        public int Id { get; set; }

        public int StockId { get; set; }

        public int ComponentId { get; set; }

        public int Count { get; set; }

        public virtual Stock Stock { get; set; }

        public virtual Component Component { get; set; }
    }
}