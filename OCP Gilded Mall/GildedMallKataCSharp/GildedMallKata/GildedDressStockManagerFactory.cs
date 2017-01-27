using System;
using System.Collections.Generic;
using GildedMallKata.AddStock;
using GildedMallKata.CheckStock;
using GildedMallKata.Reports;
using GildedMallKata.Stock;

namespace GildMallKata
{
    public class GildedDressStockManagerFactory
    {
        private readonly GildedStockManager _shop = new GildedStockManager();

        public GildedDressStockManagerFactory WithStock(List<Dress> stockItems)
        {
            stockItems.ForEach(si => new AddStockHandler(_shop).Handle(new AddStock(si)));
            return this;
        }

        public IEnumerable<StockItem> GetStockList(DateTime stockCheckDate)
        {
            return new StockCheckHandler(_shop).Handle(new CheckStockAsAtDate(stockCheckDate));
        }

        public FinancialReport FinancialReportAsAt(DateTime reportDate)
        {
            return new GenerateDressFinancialReportHandler(this).Handle(new GenerateFinancialReport(reportDate));
        }
    }
}