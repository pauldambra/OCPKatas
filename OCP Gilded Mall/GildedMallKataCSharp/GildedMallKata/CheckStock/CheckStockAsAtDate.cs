using System;

namespace GildedMallKata.CheckStock
{
    public class CheckStockAsAtDate
    {
        public DateTime StockCheckDate { get; }

        public CheckStockAsAtDate(DateTime stockCheckDate)
        {
            StockCheckDate = stockCheckDate;
        }
    }
}