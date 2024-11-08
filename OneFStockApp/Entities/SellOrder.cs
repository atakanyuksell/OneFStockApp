﻿using System.ComponentModel.DataAnnotations;

namespace OneFStockApp.Entities
{
    public class SellOrder
    {
        public Guid SellOrderID { get; set; }

        [Required]
        public string StockSymbol { get; set; }

        [Required]
        public string StockName { get; set; }

        public DateTime DateAndTimeOfOrder { get; set; }

        [Range(1, 100000, ErrorMessage = "Quantity should be between 1 and 100000.")]
        public uint Quantity { get; set; }

        [Range(1, 10000, ErrorMessage = "Quantity should be between 1 and 100000.")]
        public double Price { get; set; }
    }
}
