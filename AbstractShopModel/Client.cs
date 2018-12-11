using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AbstractShopModel
{
    /// <summary>
    /// Клиент магазина
    /// </summary>
    public class Client
    {
        public int Id { get; set; }

        [Required]
        public string ClientFIO { get; set; }

        [ForeignKey("ClientId")]
        public virtual List<Order> Orders { get; set; }
    }
}