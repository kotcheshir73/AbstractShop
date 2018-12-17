﻿using System.Runtime.Serialization;

namespace AbstractShopServiceDAL.ViewModels
{
    [DataContract]
    public class ProductComponentViewModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int ProductId { get; set; }

        [DataMember]
        public int ComponentId { get; set; }

        [DataMember]
        public string ComponentName { get; set; }

        [DataMember]
        public int Count { get; set; }
    }
}