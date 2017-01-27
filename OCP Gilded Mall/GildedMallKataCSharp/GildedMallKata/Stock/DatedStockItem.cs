using System;

namespace GildedMallKata.Stock
{
    public class DatedStockItem : StockItem
    {
        protected DateTime DateAdded { get; }

        protected DatedStockItem(DateTime dateAdded)
        {
            DateAdded = dateAdded;
        }
    }
}