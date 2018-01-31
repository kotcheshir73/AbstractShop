using System.Collections.Generic;

namespace AbstractShopService.BindingModels
{
    public class StockBindingModel
    {
        public int Id { get; set; }

        public string StockName { get; set; }

        public List<StockComponentBindingModel> StockComponents { get; set; }
    }
}
