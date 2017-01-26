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
            var stockAfterTenWeeks = GildedStockManagerFactory.GildedDress()
                                                .WithStock(stockItems, stockAdded)
                                                .GetStockList(tenWeekslater);
            stockAfterTenWeeks.Single().Name.Should().Be("A");
            stockAfterTenWeeks.Single().Price.Should().Be(10 * 0.75M);
        }
    }
}