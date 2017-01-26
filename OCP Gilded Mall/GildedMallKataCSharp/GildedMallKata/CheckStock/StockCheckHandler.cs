using System;
using System.Collections.Generic;
using System.Linq;
using GildMallKata;

namespace GildedMallKata.CheckStock
{
    public class StockCheckHandler : IHandle<CheckStockAsAtDate, IEnumerable<StockItem>>
    {
        private readonly GildedStockManager _shop;
        private static readonly TimeSpan TenWeeks = TimeSpan.FromDays(70);

        private static bool _addedTenWeeksAgo(DateTime stockCheckDate, DateTime stockDate) =>
            stockCheckDate - stockDate >= TenWeeks;

        public StockCheckHandler(GildedStockManager shop)
        {
            _shop = shop;
        }

        public IEnumerable<StockItem> Handle(CheckStockAsAtDate checkStock)
        {
            return _shop.StockList.OfType<DatedStockItem>()
                .Select(dsi => new StockItem
                {
                    Name = dsi.Name,
                    Price = _addedTenWeeksAgo(checkStock.StockCheckDate, dsi.DateAdded)
                                ? dsi.Price * 0.75M
                                : dsi.Price
                });
        }
    }
}