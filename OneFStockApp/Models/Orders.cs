using ServiceContracts;
using ServiceContracts.DTO;

namespace OneFStockApp.Models
{
    public class Orders
    {
        public List <BuyOrderResponse> BuyOrders { get; set; }

        public List <SellOrderResponse> SellOrders { get; set; }
    }
}
