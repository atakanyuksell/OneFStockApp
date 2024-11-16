using OneFStockApp;
using Services;
using OneFStockApp.Services;
using OneFStockApp.ServiceContracts;
using Microsoft.EntityFrameworkCore;



var builder = WebApplication.CreateBuilder(args);

//Services
builder.Services.AddControllersWithViews();
builder.Services.Configure<TradingOptions>(builder.Configuration.GetSection("TradingOptions"));
builder.Services.AddSingleton<IStockService, StockService>();
builder.Services.AddSingleton<IFinnhubService, FinnhubService>();
builder.Services.AddHttpClient();


var app = builder.Build();

// Add this line to allow external access
app.Urls.Add("http://*:5000");  // Dýþ IP'lerden eriþim saðlamak için


app.UseStaticFiles();
app.UseRouting();
app.MapControllers();

app.Run();

