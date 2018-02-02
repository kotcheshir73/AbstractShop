namespace AbstractShopModel
{
    /// <summary>
    /// Изделие, изготавливаемое в магазине
    /// </summary>
    public class Product
    {
        public int Id { get; set; }

        public string ProductName { get; set; }

        public decimal Price { get; set; }
    }
}
