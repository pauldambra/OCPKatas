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
            var dayStockAdded = DateTime.Now.AddDays(-100);
            var stockItems = new List<Dress> {new Dress(dayStockAdded) {Name="A", Price = 10}};

            var shop = await GildedStockManagerFactory
                                                .GildedDress()
                                                .WithStock(stockItems);

            var correlationId = Guid.NewGuid();
            var tenWeekslater = dayStockAdded.AddDays(70);
            var stockAfterTenWeeks = await shop.GenerateStockList(new GenerateStockList(correlationId, tenWeekslater));

            var stockItem = stockAfterTenWeeks.Last();
            stockItem.Name.Should().Be("A");
            stockItem.Price.Should().Be(10 * 0.75M);
        }

        [Test]
        public async Task TheGildedTinCanCannotSellYearOldPlusTins()
        {
            var oneYearAndOneDayAgo = DateTime.Now.AddYears(-1).AddDays(-1);
            var twoMonthsAgo = DateTime.Now.AddMonths(-2);
            var stock = new List<TinnedFood>
            {
                new TinnedFood(oneYearAndOneDayAgo) {Name="old", Price = 10},
                new TinnedFood(twoMonthsAgo){Name="new", Price = 10}
            };

            var shop = await GildedStockManagerFactory
                            .GildedTinCan()
                            .WithStock(stock);


            var correlationId = Guid.NewGuid();
            var stockNow = await shop.GenerateStockList(new GenerateStockList(correlationId, DateTime.Now));

            var stockItem = stockNow.Single();
            stockItem.Name.Should().Be("new");
        }
    }
}