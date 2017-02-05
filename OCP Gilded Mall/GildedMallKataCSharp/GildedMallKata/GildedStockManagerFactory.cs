using System.Collections.Generic;
using GildedMallKata;
using GildedMallKata.AddStock;
using GildedMallKata.Stock;

namespace GildMallKata
{
    public static class GildedStockManagerFactory
    {
        public static GildedDressStockManagerFactory GildedDress()
        {
            return new GildedDressStockManagerFactory();
        }

        public static GildedTinCanStockManagerFactory GildedTinCan()
        {
            return new GildedTinCanStockManagerFactory();
        }

        public static GildedStockManager WithStock(List<StockItem> stockItems)
        {
            var shop = new GildedStockManager();
            stockItems.ForEach(si => new AddStockHandler(null, null).Handle(new AddStock(si)));
            return shop;
        }
    }
}
