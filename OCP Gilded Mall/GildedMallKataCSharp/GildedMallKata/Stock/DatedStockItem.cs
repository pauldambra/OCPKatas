using System;

namespace GildedMallKata.Stock
{
    public class DatedStockItem : StockItem
    {
        public DateTime DateAdded { get; }

        protected DatedStockItem(DateTime dateAdded)
        {
            DateAdded = dateAdded;
        }
    }
}