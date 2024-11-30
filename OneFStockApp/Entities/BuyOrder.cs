using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class BuyOrder
    {
        [Key]
        public Guid BuyOrderID { get; set; }

        [Required]
        [StringLength(40)]
        public string? StockSymbol { get; set; }

        [Required]
        [StringLength(40)]
        public string? StockName { get; set; }


        public DateTime DateAndTimeOfOrder { get; set; }


        [Range(1, 100000, ErrorMessage = "Quantity should be between 1 and 100000.")]
        public uint Quantity { get; set; }

        [Required]
        [Range(1, 10000, ErrorMessage = "Quantity should be between 1 and 10000.")]
        public double Price { get; set; }
    }
}
