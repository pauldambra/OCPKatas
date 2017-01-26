using System;
using System.Collections.Generic;
using GildedMallKata.AddStock;
using GildedMallKata.CheckStock;
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
    }

    public class GildedDressStockManagerFactory
    {
        private readonly GildedStockManager _shop = new GildedStockManager();

        public GildedDressStockManagerFactory WithStock(List<Dress> stockItems)
        {
            stockItems.ForEach(si => new AddStockHandler(_shop).Handle(new AddStock(si)));
            return this;
        }

        public IEnumerable<StockItem> GetStockList(DateTime stockCheckDate)
        {
            return new StockCheckHandler(_shop).Handle(new CheckStockAsAtDate(stockCheckDate));
        }
    }

    public static class GildedStockManagerFactory
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
