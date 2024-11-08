using OneFStockApp.ServiceContracts;
using OneFStockApp.Services;
using ServiceContracts.DTO;



namespace OneFStockAppTests
{
    public class StockServiceTest
    {

        private readonly IStockService _stockService;

        //Constructor 
        public StockServiceTest()
        {
            _stockService = new StockService();
        }

        #region CreateBuyOrder

        /// <summary>
        /// When you supply BuyOrderRequest as null, it should throw ArgumentNullException.
        /// </summary>
        [Fact]
        public void CreateBuyOrder_NullRequest()
        {
            //Arrange
            BuyOrderRequest? request = null;

            //Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                //Act
                _stockService.CreateBuyOrder(request);
            });

        }

        /// <summary>
        /// When you supply BuyOrder quantity as zero, it should throw ArgumentException.
        /// </summary>
        [Fact]
        public void CreateBuyOrder_ZeroQuantity()
        {
            //Arrange
            BuyOrderRequest request = new BuyOrderRequest { Quantity = 0 };

            //Assert

            Assert.Throws<ArgumentException>(() =>
            {
                //Act
                _stockService.CreateBuyOrder(request);

            });

        }


        /// <summary>
        /// When you supply BuyOrder quantity as 100001, it should throw ArgumentException.
        /// </summary>
        [Fact]
        public void CreateBuyOrder_OverQuantity()
        {
            //Arrange
            BuyOrderRequest request = new BuyOrderRequest { Quantity = 100001 };

            //Assert

            Assert.Throws<ArgumentException>(() =>
            {
                //Act
                _stockService.CreateBuyOrder(request);

            });

        }


        /// <summary>
        /// When you supply BuyOrder price as 0, it should throw ArgumentException.
        /// </summary>
        [Fact]
        public void CreateBuyOrder_ZeroPrice()
        {
            //Arrange
            BuyOrderRequest request = new BuyOrderRequest { Price = 0 };

            //Assert

            Assert.Throws<ArgumentException>(() =>
            {
                //Act
                _stockService.CreateBuyOrder(request);

            });

        }


        /// <summary>
        /// When you supply BuyOrder price as 10001, it should throw ArgumentException.
        /// </summary>
        [Fact]
        public void CreateBuyOrder_OverPrice()
        {
            //Arrange
            BuyOrderRequest request = new BuyOrderRequest { Price = 10001 };

            //Assert

            Assert.Throws<ArgumentException>(() =>
            {
                //Act
                _stockService.CreateBuyOrder(request);

            });

        }



        /// <summary>
        /// When you supply BuyOrder stockSybol as null, it should throw ArgumentException.
        /// </summary>
        [Fact]
        public void CreateBuyOrder_StockSymbolNull()
        {
            //Arrange
            BuyOrderRequest request = new BuyOrderRequest { StockSymbol = null };

            //Assert

            Assert.Throws<ArgumentException>(() =>
            {
                //Act
                _stockService.CreateBuyOrder(request);

            });

        }


        /// <summary>
        /// When you supply BuyOrder date time as invalid, it should throw ArgumentException.
        /// </summary>
        [Fact]
        public void CreateBuyOrder_InvalidDateTime()
        {
            //Arrange
            BuyOrderRequest request = new BuyOrderRequest { DateAndTimeOfOrder = DateTime.Parse("1999-12-31") };

            //Assert

            Assert.Throws<ArgumentException>(() =>
            {
                //Act
                _stockService.CreateBuyOrder(request);

            });

        }

        /// <summary>
        /// When you supply BuyOrder as valid, it should return a buyOrderResponse type with auto-generated BuyOrderID.
        /// </summary>
        [Fact]
        public void CreateBuyOrder_ProperBuyOrderDetails()
        {
            //Arrange
            BuyOrderRequest request = new BuyOrderRequest
            {
                StockSymbol = "AAPL",
                StockName = "Apple Inc.",
                DateAndTimeOfOrder = DateTime.Now,
                Quantity = 100,
                Price = 150.50
            };


            //Act
            BuyOrderResponse buyOrderResponseFromCreate = _stockService.CreateBuyOrder(request);

            //Assert
            Assert.NotEqual(Guid.Empty, buyOrderResponseFromCreate.BuyOrderID);




        }

        #endregion

        #region GetAllBuyOrders

        /// <summary>
        /// When you invoke this method, by default, the returned list should be empty.
        /// </summary>
        [Fact]
        public void GetAllBuyOrders_Default()
        {
            //Act
            List<BuyOrderResponse> listFromService = _stockService.GetBuyOrders();

            //Assert
            Assert.Empty(listFromService);
        }

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void GetBuyOrders_AddBuyOrders()
        {
            //Arrange
            List<BuyOrderRequest> buyOrderRequestList = new List<BuyOrderRequest>
            {

                   new BuyOrderRequest
                   {
                       StockSymbol = "AAPL",
                       StockName = "Apple Inc.",
                       DateAndTimeOfOrder = DateTime.ParseExact("2023-01-15", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture),
                       Quantity = 50,
                       Price = 150.75
                   },


                   new BuyOrderRequest
                   {
                       StockSymbol = "TSLA",
                       StockName = "Tesla, Inc.",
                       DateAndTimeOfOrder = DateTime.ParseExact("2023-05-20", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture),
                       Quantity = 100,
                       Price = 850.25
                   },


                   new BuyOrderRequest
                   {
                       StockSymbol = "MSFT",
                       StockName = "Microsoft Corporation",
                       DateAndTimeOfOrder = DateTime.ParseExact("2023-08-10", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture),
                       Quantity = 200,
                       Price = 300.50
                   },


                   new BuyOrderRequest
                   {
                       StockSymbol = "AMZN",
                       StockName = "Amazon.com, Inc.",
                       DateAndTimeOfOrder = DateTime.ParseExact("2023-12-05", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture),
                       Quantity = 75,
                       Price = 100.00
                   }
            };

            //List<BuyOrderResponse> buyOrdResListFromAdd = buyOrderRequestList(temp => _stockService.CreateBuyOrder(temp));

            List<BuyOrderResponse> buyOrdResListFromAdd = new List<BuyOrderResponse>();
            List<BuyOrderResponse> buyOrdResListFromGet = new List<BuyOrderResponse>();

            foreach (BuyOrderRequest req in buyOrderRequestList)
            {
                buyOrdResListFromAdd.Add(_stockService.CreateBuyOrder(req));

            }

            buyOrdResListFromAdd = buyOrdResListFromAdd.OrderByDescending(temp => temp.DateAndTimeOfOrder).ToList();

            //Act
            buyOrdResListFromGet = _stockService.GetBuyOrders();


            //Assert
            Assert.Equal(buyOrdResListFromAdd, buyOrdResListFromGet);

        }
        #endregion

        #region GetAllSellOrders

        [Fact]
        public void GetAllSellOrders_Default()
        {
            //Arrange
            List<SellOrderResponse> listFromService = _stockService.GetSellOrders();

            //Act
            Assert.Empty(listFromService);

            //Assert
        }



        public void GetBuyOrders_AddSellOrder()
        {
            //Arrange
            List<SellOrderRequest> sellOrderRequest = new List<SellOrderRequest>
            {

                   new SellOrderRequest
                   {
                       StockSymbol = "AAPL",
                       StockName = "Apple Inc.",
                       DateAndTimeOfOrder = DateTime.ParseExact("2023-01-15", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture),
                       Quantity = 50,
                       Price = 150.75
                   },


                   new SellOrderRequest
                   {
                       StockSymbol = "TSLA",
                       StockName = "Tesla, Inc.",
                       DateAndTimeOfOrder = DateTime.ParseExact("2023-05-20", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture),
                       Quantity = 100,
                       Price = 850.25
                   },


                   new SellOrderRequest
                   {
                       StockSymbol = "MSFT",
                       StockName = "Microsoft Corporation",
                       DateAndTimeOfOrder = DateTime.ParseExact("2023-08-10", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture),
                       Quantity = 200,
                       Price = 300.50
                   },


                   new SellOrderRequest
                   {
                       StockSymbol = "AMZN",
                       StockName = "Amazon.com, Inc.",
                       DateAndTimeOfOrder = DateTime.ParseExact("2023-12-05", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture),
                       Quantity = 75,
                       Price = 100.00
                   }
            };

            //List<BuyOrderResponse> buyOrdResListFromAdd = buyOrderRequestList(temp => _stockService.CreateBuyOrder(temp));

            List<SellOrderResponse> sellOrderResListFromAdd = new List<SellOrderResponse>();
            List<SellOrderResponse> sellOrderResListFromGet = new List<SellOrderResponse>();

            foreach (SellOrderRequest req in sellOrderRequest)
            {
                sellOrderResListFromAdd.Add(_stockService.CreateSellOrder(req));

            }

            sellOrderResListFromAdd = sellOrderResListFromAdd.OrderByDescending(temp => temp.DateAndTimeOfOrder).ToList();

            //Act
            sellOrderResListFromGet = _stockService.GetSellOrders();


            //Assert
            Assert.Equal(sellOrderResListFromAdd, sellOrderResListFromGet);

        }

        #endregion


    }

}