using System;
using System.Collections.Generic;
using GildedMallKata.Stock;
using GildMallKata;

namespace GildedMallKata.CheckStock
{
    public class GenerateStockList : Command
    {
        public Guid CorrelationId { get; }
        public DateTime StockCheckDate { get; }

        public GenerateStockList(Guid correlationId, DateTime stockCheckDate)
        {
            CorrelationId = correlationId;
            StockCheckDate = stockCheckDate;
        }
    }

    public class StockListGenerated : Event
    {
        public Guid CorrelationId { get; }
        public IEnumerable<StockItem> StockList { get; }

        public StockListGenerated(Guid correlationId, IEnumerable<StockItem> stockList)
        {
            CorrelationId = correlationId;
            StockList = stockList;
        }
    }
}