using Entities;
using OneFStockApp.Entities;
using ServiceContracts;
using ServiceContracts.DTO;
using Services.Helpers;
using OneFStockApp.ServiceContracts;

namespace OneFStockApp.Services
{
    public class StockService : IStockService
    {

        private readonly List<BuyOrder> _buyOrders;

        private readonly List<SellOrder> _sellOrders;

        /// <summary>
        /// StockService başlatıldığında çalışacak constructor.Bu consturctor sayesinde boş listelerimiz oluşturulur.
        /// </summary>
        public StockService()
        {
            _buyOrders = new List<BuyOrder>();
            _sellOrders = new List<SellOrder>();
        }


        public BuyOrderResponse CreateBuyOrder(BuyOrderRequest? buyOrderRequest)
        {

            if (buyOrderRequest == null)
            {
                throw new ArgumentNullException(nameof(buyOrderRequest));
            }


            // BuyOrderRequest'i doğrulama işlemi.
            ValidationHelper.ModelValidation(buyOrderRequest);

            /// Request'i doğrudan BuyOrder nesnesine çeviriyoruz.
            BuyOrder buyOrder = buyOrderRequest.ToBuyOrder();

            // BuyOrder id'si harici olarak atanıyor.
            buyOrder.BuyOrderID = Guid.NewGuid();

            // buyOrder'ı _buyOrders listesine ekliyoruz.
            _buyOrders.Add(buyOrder);

            return buyOrder.ToBuyOrderResponse();

        }

        public SellOrderResponse CreateSellOrder(SellOrderRequest? sellOrderRequest)
        {
            if (sellOrderRequest == null)
            {
                throw new ArgumentNullException(nameof(sellOrderRequest));
            }

            ValidationHelper.ModelValidation(sellOrderRequest);

            SellOrder sellOrder = sellOrderRequest.ToSellOrder();

            sellOrder.SellOrderID = Guid.NewGuid();

            _sellOrders.Add(sellOrder);

            return sellOrder.ToSellOrderResponse();

        }

        public List<BuyOrderResponse> GetBuyOrders()
        {
            return _buyOrders.OrderByDescending(temp => temp.DateAndTimeOfOrder).Select(temp => temp.ToBuyOrderResponse()).ToList();
        }

        public List<SellOrderResponse> GetSellOrders()
        {
            return _sellOrders.OrderByDescending(temp => temp.DateAndTimeOfOrder).Select(temp => temp.ToSellOrderResponse()).ToList();
        }
    }

}

