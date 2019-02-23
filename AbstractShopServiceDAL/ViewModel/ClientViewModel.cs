using System.ComponentModel;

namespace AbstractShopServiceDAL.ViewModels
{
    public class ClientViewModel
    {
        public int Id { get; set; }

        [DisplayName("ФИО Клиента")]
        public string ClientFIO { get; set; }
    }
}