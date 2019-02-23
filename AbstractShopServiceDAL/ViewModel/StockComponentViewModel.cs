using System.ComponentModel;

namespace AbstractShopServiceDAL.ViewModels
{
    public class StockComponentViewModel
    {
        public int Id { get; set; }

        public int StockId { get; set; }

        public int ComponentId { get; set; }

        [DisplayName("Название компонента")]
        public string ComponentName { get; set; }

        [DisplayName("Количество")]
        public int Count { get; set; }
    }
}