using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using EventStore.ClientAPI;
using GildedMallKata.AddStock;
using GildedMallKata.CheckStock;
using GildedMallKata.Reports;
using GildedMallKata.Stock;

namespace GildMallKata
{
    public class GildedDressStockManagerFactory
    {
        private readonly GildedStockManager _shop = new GildedStockManager();
        private readonly IEventStoreConnection _connection;
        private readonly Task _connectedToEventStore;
        public const string ShopStream = "GildedDress";

        public GildedDressStockManagerFactory()
        {
            _connection = EventStoreConnection.Create(new IPEndPoint(IPAddress.Loopback, 1113));
            _connectedToEventStore = _connection.ConnectAsync();
        }

        public async Task<GildedDressStockManagerFactory> WithStock(IEnumerable<Dress> stockItems)
        {
            await _connectedToEventStore;
            var writes = stockItems.Select(si => new AddStockHandler(_connection, ShopStream).Handle(new AddStock(si)));
            await Task.WhenAll(writes);
            return this;
        }

        public async Task GenerateStockList(GenerateStockList generateStockList)
        {
            await _connectedToEventStore;
            new StockCheckHandler(_connection, ShopStream).Handle(generateStockList);
        }

//        public async Task<IEnumerable<StockItem>> GetStockList(DateTime stockCheckDate)
//        {
//            await _connectedToEventStore;
////            return new StockCheckHandler(_shop).Handle(new CheckStockAsAtDate(stockCheckDate));
//            return new List<StockItem>().AsEnumerable();
//        }

        public async Task<FinancialReport> FinancialReportAsAt(DateTime reportDate)
        {
            await _connectedToEventStore;
//            return new GenerateDressFinancialReportHandler(this).Handle(new GenerateFinancialReport(reportDate));
            return null;
        }
    }
}