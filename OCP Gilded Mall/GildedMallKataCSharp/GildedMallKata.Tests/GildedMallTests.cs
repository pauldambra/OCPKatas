using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using GildedMallKata.Stock;
using GildMallKata;
using NUnit.Framework;

namespace GildedMallKata.Tests
{
    [TestFixture]
    public class GildedMallTests
    {
        [Test]
        public void AllTenantsWantToRecordItemNameAndPrice()
        {
            var stockItems = new List<StockItem> {new StockItem {Name="A", Price = 10}};
            var shop = GildedStockManagerFactory.WithStock(stockItems);
            shop.StockList.Single().Name.Should().Be("A");
        }

        [Test]
        public void TheGildedDressDecreasesPriceAfterTenWeeks()
        {
            var stockAdded = DateTime.Now.AddDays(-100);
            var stockItems = new List<Dress> {new Dress(stockAdded) {Name="A", Price = 10}};

            var tenWeekslater = stockAdded.AddDays(70);
            var stockAfterTenWeeks = GildedStockManagerFactory
                                                .GildedDress()
                                                .WithStock(stockItems)
                                                .GetStockList(tenWeekslater);

            var stockItem = stockAfterTenWeeks.Single();
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