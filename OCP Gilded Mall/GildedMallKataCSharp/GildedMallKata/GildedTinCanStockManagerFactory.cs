using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using EventStore.ClientAPI;
using GildedMallKata.AddStock;
using GildedMallKata.CheckStock;
using GildedMallKata.Reports;
using GildedMallKata.Stock;
using Newtonsoft.Json;

namespace GildedMallKata
{
    public class GildedTinCanStockManagerFactory
    {
        private readonly IEventStoreConnection _connection;
        private readonly Task _connectedToEventStore;
        private const string ShopStream = "GildedTinCan";

        public GildedTinCanStockManagerFactory()
        {
            _connection = EventStoreConnection.Create(new IPEndPoint(IPAddress.Loopback, 1113));
            _connectedToEventStore = _connection.ConnectAsync();
        }

        public async Task<GildedTinCanStockManagerFactory> WithStock(IEnumerable<TinnedFood> stockItems)
        {
            await _connectedToEventStore;
            var writes = stockItems.Select(si => new AddStockHandler(_connection, ShopStream).Handle(new AddStock.AddStock(si)));
            await Task.WhenAll(writes);
            return this;
        }

        public async Task<IEnumerable<StockItem>> GenerateStockList(GenerateStockList generateStockList)
        {
            await _connectedToEventStore;
            var receivedList = new TaskCompletionSource<IEnumerable<StockItem>>();

            SubscribeForResult(generateStockList, receivedList);

            await new GildedTinCanStockExpiryHandler(_connection, ShopStream).Handle(generateStockList);

            await CompleteOrTimeout(receivedList);

            return await receivedList.Task;
        }

        private static async Task CompleteOrTimeout(TaskCompletionSource<IEnumerable<StockItem>> receivedList)
        {
            await Task.WhenAny(receivedList.Task, Task.Delay(30000));

            if (receivedList.Task.IsFaulted || !receivedList.Task.IsCompleted)
            {
                throw receivedList.Task.Exception ?? new Exception("timed out waiting for a stock list");
            }
        }

        private void SubscribeForResult(GenerateStockList generateStockList, TaskCompletionSource<IEnumerable<StockItem>> receivedList)
        {
            _connection.SubscribeToStreamAsync(
                ShopStream,
                true,
                (subscription, resolvedEvent) =>
                {
                    var stockListGenerated =
                        JsonConvert.DeserializeObject<StockListGenerated>(Encoding.UTF8.GetString(resolvedEvent.Event.Data));
                    if (stockListGenerated.CorrelationId == generateStockList.CorrelationId)
                    {
                        receivedList.SetResult(stockListGenerated.StockList);
                    }
                }
            );
        }

        public IEnumerable<StockItem> GetStockList(DateTime stockCheckDate)
        {
//            return new StockExpiryHandler(_shop).Handle(new CheckStockAsAtDate(stockCheckDate));
            return null;
        }


        public FinancialReport FinancialReportAsAt(DateTime reportDate)
        {
//            return new GenerateTinCanFinancialReportHandler(this).Handle(new GenerateFinancialReport(reportDate));
            return null;
        }
    }
}