using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OneFStockApp.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Buy_Orders",
                columns: table => new
                {
                    BuyOrderID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StockSymbol = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    StockName = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    DateAndTimeOfOrder = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Quantity = table.Column<long>(type: "bigint", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buy_Orders", x => x.BuyOrderID);
                });

            migrationBuilder.CreateTable(
                name: "Sell_Orders",
                columns: table => new
                {
                    SellOrderID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StockSymbol = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    StockName = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    DateAndTimeOfOrder = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Quantity = table.Column<long>(type: "bigint", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sell_Orders", x => x.SellOrderID);
                });

            migrationBuilder.InsertData(
                table: "Buy_Orders",
                columns: new[] { "BuyOrderID", "DateAndTimeOfOrder", "Price", "Quantity", "StockName", "StockSymbol" },
                values: new object[,]
                {
                    { new Guid("169db619-99e1-41a4-a096-7b00f1265205"), new DateTime(2023, 12, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 19.609999999999999, 20L, "Ivy NextShares", "IVENC" },
                    { new Guid("269729a5-bab7-4419-a157-27f7ab29f39e"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 45.850000000000001, 75L, "Autobytel Inc.", "ABTL" },
                    { new Guid("4ca8e76d-4a96-4017-81f9-06aba4386d8f"), new DateTime(2024, 5, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 22.0, 81L, "Nuveen Insured California Select", "NXC" },
                    { new Guid("5ec384c9-49d4-423b-a9ba-bde631a3dea3"), new DateTime(2024, 5, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 86.170000000000002, 31L, "Fortress Transportation and", "FTAI" },
                    { new Guid("833d2fb0-532e-4aff-b0ca-95ea8a4c0f7b"), new DateTime(2024, 7, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 48.409999999999997, 18L, "Insperity, Inc.", "NSP" },
                    { new Guid("8d6d5b7c-b06d-4afe-bb5a-1b33d3d52757"), new DateTime(2024, 8, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 97.930000000000007, 4L, "Inotek Pharmaceuticals Corporation", "ITEK" },
                    { new Guid("950706d9-a6a2-425f-a937-a89aac7433e4"), new DateTime(2024, 5, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 30.109999999999999, 92L, "First Trust Indxx ETF", "FTAG" },
                    { new Guid("b770b401-914b-4c0a-bf70-7adf01e5c6cc"), new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 84.810000000000002, 61L, "Diplomat Pharmacy, Inc.", "DPLO" },
                    { new Guid("d534ead7-1b0c-4ac7-b7af-82782531482a"), new DateTime(2024, 4, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 55.340000000000003, 85L, "pSivida Corp.", "PSDV" },
                    { new Guid("f618366a-6f7c-4d9b-826f-e3afdbe6d8f3"), new DateTime(2024, 10, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 86.909999999999997, 90L, "Extraction Oil & Gas, Inc.", "XOG" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Buy_Orders");

            migrationBuilder.DropTable(
                name: "Sell_Orders");
        }
    }
}
