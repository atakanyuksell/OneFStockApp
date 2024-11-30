using OneFStockApp;
using Services;
using OneFStockApp.Services;
using OneFStockApp.ServiceContracts;
using Microsoft.EntityFrameworkCore;
using OneFStockApp.Entities;
using Entities;


var builder = WebApplication.CreateBuilder(args);

//Services
builder.Services.AddControllersWithViews();
builder.Services.Configure<TradingOptions>(builder.Configuration.GetSection("TradingOptions"));
builder.Services.AddScoped<IStockService, StockService>();
builder.Services.AddScoped<IFinnhubService, FinnhubService>();
builder.Services.AddHttpClient();
builder.Services.AddDbContext<OrderDbContext>(
    options => {
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
        
    } );


var app = builder.Build();

// Add this line to allow external access
app.Urls.Add("http://*:5000");  // Dýþ IP'lerden eriþim saðlamak için


app.UseStaticFiles();
app.UseRouting();
app.MapControllers();

app.Run();

