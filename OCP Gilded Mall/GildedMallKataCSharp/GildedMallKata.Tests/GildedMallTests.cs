using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
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
            var stockItems = new List<StockItem> {new StockItem {Name="A", Price = 10}};
            var stockAdded = DateTime.Now.AddDays(-100);
            var tenWeekslater = stockAdded.AddDays(70);
            var stockAfterTenWeeks = GildedStockManagerFactory
                                                .GildedDress()
                                                .WithStock(stockItems, stockAdded)
                                                .GetStockList(tenWeekslater);
            var stockItem = stockAfterTenWeeks.Single();
            stockItem.Name.Should().Be("A");
            stockItem.Price.Should().Be(10 * 0.75M);
        }

        [Test]
        public void TheGildedTinCanCannotSellYearOldPlusTins()
        {
            var oldestStock = new List<StockItem> {new StockItem {Name="old", Price = 10}};
            var newerStock = new List<StockItem> {new StockItem {Name="new", Price = 10}};
            var oneYearAndOneDayAgo = DateTime.Now.AddYears(-1).AddDays(-1);
            var twoMonthsAgo = DateTime.Now.AddMonths(-2);
            var stockNow = GildedStockManagerFactory
                            .GildedTinCan()
                            .WithStock(oldestStock, oneYearAndOneDayAgo)
                            .WithStock(newerStock, twoMonthsAgo)
                            .GetStockList(DateTime.Now);
            var stockItem = stockNow.Single();
            stockItem.Name.Should().Be("new");
        }
    }
}