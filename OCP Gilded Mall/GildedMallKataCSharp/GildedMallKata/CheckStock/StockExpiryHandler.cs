using System;
using System.Collections.Generic;
using System.Linq;
using GildMallKata;

namespace GildedMallKata.CheckStock
{
    public class StockExpiryHandler : IHandle<CheckStockAsAtDate, IEnumerable<StockItem>>
    {
        private readonly GildedStockManager _shop;
        private static readonly TimeSpan OneYear = TimeSpan.FromDays(365.25);

        private static bool _lessThanAYearOld(DateTime stockCheckDate, DateTime stockDate) =>
            stockCheckDate - stockDate <= OneYear;

        public StockExpiryHandler(GildedStockManager shop)
        {
            _shop = shop;
        }

        public IEnumerable<StockItem> Handle(CheckStockAsAtDate checkStock)
        {
            return _shop.StockList
                .OfType<DatedStockItem>()
                .Where(dsi => _lessThanAYearOld(checkStock.StockCheckDate, dsi.DateAdded));
        }
    }
}