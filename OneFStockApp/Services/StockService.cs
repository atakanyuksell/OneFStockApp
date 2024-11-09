using Entities;
using OneFStockApp.Entities;
using OneFStockApp.ServiceContracts;
using ServiceContracts.DTO;
using Services.Helpers;

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
            _buyOrders = new List<BuyOrder>() {
                new BuyOrder
                {
                    BuyOrderID = Guid.NewGuid(),
                    StockSymbol = "AAPL",
                    StockName = "Apple Inc.",
                    DateAndTimeOfOrder = DateTime.Parse("2023-05-01"),
                    Quantity = 5000,
                    Price = 150.50
                },
                new BuyOrder
                {
                    BuyOrderID = Guid.NewGuid(),
                    StockSymbol = "TSLA",
                    StockName = "Tesla, Inc.",
                    DateAndTimeOfOrder = DateTime.Parse("2023-06-15"),
                    Quantity = 10000,
                    Price = 700.75
                }
            };

            _sellOrders = new List<SellOrder>()
            {
                new SellOrder
                {
                    SellOrderID = Guid.NewGuid(),
                    StockSymbol = "GOOGL",
                    StockName = "Alphabet Inc.",
                    DateAndTimeOfOrder = DateTime.Parse("2023-09-20"),
                    Quantity = 3000,
                    Price = 1250.00
                },
                new SellOrder
                {
                    SellOrderID = Guid.NewGuid(),
                    StockSymbol = "AMZN",
                    StockName = "Amazon.com, Inc.",
                    DateAndTimeOfOrder = DateTime.Parse("2023-11-10"),
                    Quantity = 2000,
                    Price = 2000.50
                }

            };
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

