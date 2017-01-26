using System;
using System.Collections.Generic;
using FluentAssertions;
using GildedMallKata.Stock;
using GildMallKata;
using NUnit.Framework;

namespace GildedMallKata.Tests
{
    [TestFixture]
    public class FinancialReportsTests
    {
        [Test]
        public void GildedDressCanGenerateZeroDepreciation()
        {
            var stockAdded = DateTime.Now.AddDays(-1);
            var stockItems = new List<Dress> {new Dress(stockAdded) {Name="A", Price = 10}};

            var financialReport = GildedStockManagerFactory
                .GildedDress()
                .WithStock(stockItems)
                .FinancialReportAsAt(DateTime.Now);

            financialReport.ValueOfStock.Should().Be(10);
            financialReport.Depreciation.Should().Be(0);
        }

        [Test]
        public void GildedDressCanGenerateCorrectDepreciation()
        {
            var thisMonth = DateTime.Now.AddDays(-1);
            var addedThisMonth = new Dress(thisMonth) {Name="A", Price = 10};

            var tenWeeksAgo = DateTime.Now.AddDays(-70).AddTicks(-1);
            var priceReducesThisMonth = new Dress(tenWeeksAgo) {Name="A", Price = 10};

            var stockItems = new List<Dress> {addedThisMonth, priceReducesThisMonth};

            var financialReport = GildedStockManagerFactory
                .GildedDress()
                .WithStock(stockItems)
                .FinancialReportAsAt(DateTime.Now);

            financialReport.ValueOfStock.Should().Be(17.5M);
            financialReport.Depreciation.Should().Be(2.5M);
        }

        [Test]
        public void GildedTinCanCanGenerateCorrectDepreciation()
        {
            var twoYearsAgo = DateTime.Now.AddYears(-2);
            var expiredBeforeThisReport = new TinnedFood(twoYearsAgo) {Name="A", Price = 10};

            var oneYearAgo = DateTime.Now.AddYears(-1).AddTicks(-1);
            var expiredDuringThisReport = new TinnedFood(oneYearAgo) {Name="A", Price = 15};

            var yesterday = DateTime.Now.AddDays(-1);
            var notExpired = new TinnedFood(yesterday) {Name="A", Price = 20};

            var stockItems = new List<TinnedFood>
            {
                expiredBeforeThisReport,
                expiredDuringThisReport,
                notExpired
            };

            var financialReport = GildedStockManagerFactory
                .GildedTinCan()
                .WithStock(stockItems)
                .FinancialReportAsAt(DateTime.Now);

            financialReport.ValueOfStock.Should().Be(20M);
            financialReport.Depreciation.Should().Be(15M);
        }
    }
}