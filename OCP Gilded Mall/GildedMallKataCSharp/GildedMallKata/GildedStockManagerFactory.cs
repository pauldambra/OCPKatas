using System;
using System.Collections.Generic;
using GildedMallKata.AddStock;
using GildedMallKata.CheckStock;

namespace GildMallKata
{
    public class GildedTinCanStockManagerFactory
    {
        private readonly GildedStockManager _shop = new GildedStockManager();

        public GildedTinCanStockManagerFactory WithStock(List<StockItem> stockItems, DateTime stockAdded)
        {
            stockItems.ForEach(si => new AddDatedStockHandler(_shop, stockAdded).Handle(new AddStock(si)));
            return this;
        }

        public IEnumerable<StockItem> GetStockList(DateTime stockCheckDate)
        {
            return new StockExpiryHandler(_shop).Handle(new CheckStockAsAtDate(stockCheckDate));
        }
    }

    public class GildedDressStockManagerFactory
    {
        private readonly GildedStockManager _shop = new GildedStockManager();

        public GildedDressStockManagerFactory WithStock(List<StockItem> stockItems, DateTime stockAdded)
        {
            stockItems.ForEach(si => new AddDatedStockHandler(_shop, stockAdded).Handle(new AddStock(si)));
            return this;
        }

        public IEnumerable<StockItem> GetStockList(DateTime stockCheckDate)
        {
            return new StockCheckHandler(_shop).Handle(new CheckStockAsAtDate(stockCheckDate));
        }
    }

    public class GildedStockManagerFactory
    {
      public static GildedStockManager Create()
        {
            return new GildedStockManager ();
        }

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
            stockItems.ForEach(si => new AddStockHandler(shop).Handle(new AddStock(si)));
            return shop;
        }
    }
}
