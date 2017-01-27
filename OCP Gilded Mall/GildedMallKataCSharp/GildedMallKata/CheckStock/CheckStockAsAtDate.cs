using System;
using GildMallKata;

namespace GildedMallKata.CheckStock
{
    public class CheckStockAsAtDate : Command
    {
        public DateTime StockCheckDate { get; }

        public CheckStockAsAtDate(DateTime stockCheckDate)
        {
            StockCheckDate = stockCheckDate;
        }
    }
}