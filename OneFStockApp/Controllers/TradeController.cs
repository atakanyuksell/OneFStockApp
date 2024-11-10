using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ServiceContracts;
using OneFStockApp;
using static OneFStockApp.TradingOptions;
using OneFStockApp.Models;
using OneFStockApp.Services;
using OneFStockApp.ServiceContracts;
using ServiceContracts.DTO;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.Diagnostics;


namespace OneFStockApp.Controllers
{
    [Route("[controller]")]
    public class TradeController : Controller
    {
        private readonly TradingOptions _tradingOptions;
        private readonly IStockService _stocksService;
        private readonly IFinnhubService _finnhubService;
        private readonly IConfiguration _configuration;


        /// <summary>
        /// Constructor for TradeController that executes when a new object is created for the class
        /// </summary>
        /// <param name="tradingOptions">Injecting TradeOptions config through Options pattern</param>
        /// <param name="stocksService">Injecting StocksService</param>
        /// <param name="finnhubService">Injecting FinnhubService</param>
        /// <param name="configuration">Injecting IConfiguration</param>
        public TradeController(IOptions<TradingOptions> tradingOptions, IStockService stocksService, IFinnhubService finnhubService, IConfiguration configuration)
        {
            _tradingOptions = tradingOptions.Value;
            _stocksService = stocksService;
            _finnhubService = finnhubService;
            _configuration = configuration;
        }


        [Route("/")]
        [Route("[action]")]
        [Route("~/[controller]")]
        public IActionResult Index()
        {
            //reset stock symbol if not exists
            if (string.IsNullOrEmpty(_tradingOptions.DefaultStockSymbol))
                _tradingOptions.DefaultStockSymbol = "MSFT";


            //get company profile from API server
            Dictionary<string, object>? companyProfileDictionary = _finnhubService.GetCompanyProfile(_tradingOptions.DefaultStockSymbol);

            //get stock price quotes from API server
            Dictionary<string, object>? stockQuoteDictionary = _finnhubService.GetStockPriceQuote(_tradingOptions.DefaultStockSymbol);


            //create model object
            StockTrade stockTrade = new StockTrade() { StockSymbol = _tradingOptions.DefaultStockSymbol };

            //load data from finnHubService into model object
            if (companyProfileDictionary != null && stockQuoteDictionary != null)
            {
                stockTrade = new StockTrade() { StockSymbol = Convert.ToString(companyProfileDictionary["ticker"]), StockName = Convert.ToString(companyProfileDictionary["name"]), Price = Convert.ToDouble(stockQuoteDictionary["c"].ToString()) };
            }

            //Send Finnhub token to view
            ViewBag.FinnhubToken = _configuration["FinnhubToken"];

            return View(stockTrade);
        }


        [Route("/Orders")]
        public IActionResult Orders()
        {
            // Create a Orders model.
            

            // Get all the buy orders.
            List <BuyOrderResponse> buyOrderResponses =_stocksService.GetBuyOrders();

            //Get all the sell orders.
            List <SellOrderResponse> sellOrderResponses =_stocksService.GetSellOrders();

            //Declare the orders data.
            Orders orders = new Orders() { BuyOrders  = buyOrderResponses, SellOrders = sellOrderResponses };

            //We passed orders data as strongly typed data.
            return View(orders);

        }
 
        [Route("[action]")]
        [HttpPost]
        public IActionResult BuyOrder(BuyOrderRequest request)
        {
            //Adjust all the values related with stockSymbol.
            Dictionary <string,object>?  CompDetailsDictionary = _finnhubService.GetCompanyProfile(request.StockSymbol);
            Dictionary <string, object>? stockQuoteDictionary = _finnhubService.GetStockPriceQuote(request.StockSymbol);

            

            if (CompDetailsDictionary != null && stockQuoteDictionary != null && CompDetailsDictionary["name"] != null)
            {

                request.StockName = Convert.ToString(CompDetailsDictionary["name"]) ?? string.Empty;
                request.DateAndTimeOfOrder = DateTime.Now;
                request.Price = Convert.ToDouble(stockQuoteDictionary["c"].ToString());

            }
            //Create a buy order.
            BuyOrderResponse response = _stocksService.CreateBuyOrder(request);


            //BuyOrderResponse response = _stocksService.CreateBuyOrder();
            return RedirectToAction("Index");

        }


        [Route("[action]")]
        [HttpPost]
        public IActionResult SellOrder(SellOrderRequest request)
        {

            //Adjust all the values related with stockSymbol.
            Dictionary<string, object>? CompDetailsDictionary = _finnhubService.GetCompanyProfile(request.StockSymbol);
            Dictionary<string, object>? stockQuoteDictionary = _finnhubService.GetStockPriceQuote(request.StockSymbol);



            if (CompDetailsDictionary != null && stockQuoteDictionary != null && CompDetailsDictionary["name"] != null)
            {

                request.StockName = Convert.ToString(CompDetailsDictionary["name"]) ?? string.Empty;
                request.DateAndTimeOfOrder = DateTime.Now;
                request.Price = Convert.ToDouble(stockQuoteDictionary["c"].ToString());

            }
            //Create a buy order.
            SellOrderResponse response = _stocksService.CreateSellOrder(request);


            //BuyOrderResponse response = _stocksService.CreateBuyOrder();
            return RedirectToAction("Index");
        }


    }
}

