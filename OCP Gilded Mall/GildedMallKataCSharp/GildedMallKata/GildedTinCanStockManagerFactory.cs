using System;
using System.Collections.Generic;
using GildedMallKata.AddStock;
using GildedMallKata.CheckStock;
using GildedMallKata.Reports;
using GildedMallKata.Stock;

namespace GildMallKata
{
    public class GildedTinCanStockManagerFactory
    {
        private readonly GildedStockManager _shop = new GildedStockManager();

        public GildedTinCanStockManagerFactory WithStock(List<TinnedFood> stockItems)
        {
            var addTinnedFoodHandler = new AddStockHandler(_shop);
            stockItems.ForEach(si =>
            {
                addTinnedFoodHandler.Handle(new AddStock(si));
            });
            return this;
        }

        public IEnumerable<StockItem> GetStockList(DateTime stockCheckDate)
        {
            return new StockExpiryHandler(_shop).Handle(new CheckStockAsAtDate(stockCheckDate));
        }

        public FinancialReport FinancialReportAsAt(DateTime reportDate)
        {
            return new GenerateTinCanFinancialReportHandler(this).Handle(new GenerateFinancialReport(reportDate));
        }
    }
}