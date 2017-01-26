using System;

namespace GildedMallKata.Stock
{
    public class DatedStockItem : StockItem
    {
        public DateTime DateAdded { get; }

        public DatedStockItem(DateTime dateAdded)
        {
            DateAdded = dateAdded;
        }
    }
}