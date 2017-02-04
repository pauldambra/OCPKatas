using System;
using GildedMallKata.Stock;
using GildMallKata;

namespace GildedMallKata.AddStock
{
    public class AddStock : Command
    {
        public StockItem StockItem { get; }

        public AddStock(StockItem stockItem)
        {
            StockItem = stockItem;
        }
    }

    public class StockAdded : Event
    {
        public DateTime DateAdded { get; }
        public StockItem StockItem { get; }

        public StockAdded(DateTime dateAdded, StockItem stockItem)
        {
            DateAdded = dateAdded;
            StockItem = stockItem;
        }
    }
}