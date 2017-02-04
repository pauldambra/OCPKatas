using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using EventStore.ClientAPI;
using FluentAssertions;
using GildedMallKata.CheckStock;
using GildedMallKata.Stock;
using GildMallKata;
using Newtonsoft.Json;
using NUnit.Framework;

namespace GildedMallKata.Tests
{
    [TestFixture]
    public class GildedMallTests
    {
        [Test]
        public async Task TheGildedDressDecreasesPriceAfterTenWeeks()
        {
            var stockAdded = DateTime.Now.AddDays(-100);
            var stockItems = new List<Dress> {new Dress(stockAdded) {Name="A", Price = 10}};

            var tenWeekslater = stockAdded.AddDays(70);
            var shop = await GildedStockManagerFactory
                                                .GildedDress()
                                                .WithStock(stockItems);

            //first we subscribe
            var connection = EventStoreConnection.Create(new IPEndPoint(IPAddress.Loopback, 1113));
            await connection.ConnectAsync();

            var receivedList = new TaskCompletionSource<IEnumerable<StockItem>>();

            var correlationId = Guid.NewGuid();

            await connection.SubscribeToStreamAsync(
                GildedDressStockManagerFactory.ShopStream,
                true,
                (subscription, resolvedEvent) =>
                {
                    var stockListGenerated = JsonConvert.DeserializeObject<StockListGenerated>(Encoding.UTF8.GetString(resolvedEvent.Event.Data));
                    if (stockListGenerated.CorrelationId == correlationId)
                    {
                        receivedList.SetResult(stockListGenerated.StockList);
                    }
                }
            );
            // this eventually causes a StockListGenerated event with the expected correlation id

            await shop.GenerateStockList(new GenerateStockList(correlationId, tenWeekslater));

            await Task.WhenAny(receivedList.Task, Task.Delay(30000));

            if (receivedList.Task.IsFaulted || !receivedList.Task.IsCompleted)
            {
                throw receivedList.Task.Exception ?? new Exception("timed out waiting for a stock list");
            }

            var stockAfterTenWeeks = await receivedList.Task;

            var stockItem = stockAfterTenWeeks.Last();
            stockItem.Name.Should().Be("A");
            stockItem.Price.Should().Be(10 * 0.75M);
        }

        [Test]
        public void TheGildedTinCanCannotSellYearOldPlusTins()
        {
            var oneYearAndOneDayAgo = DateTime.Now.AddYears(-1).AddDays(-1);
            var twoMonthsAgo = DateTime.Now.AddMonths(-2);
            var stock = new List<TinnedFood>
            {
                new TinnedFood(oneYearAndOneDayAgo) {Name="old", Price = 10},
                new TinnedFood(twoMonthsAgo){Name="new", Price = 10}
            };

            var stockNow = GildedStockManagerFactory
                            .GildedTinCan()
                            .WithStock(stock)
                            .GetStockList(DateTime.Now);

            var stockItem = stockNow.Single();
            stockItem.Name.Should().Be("new");
        }
    }
}