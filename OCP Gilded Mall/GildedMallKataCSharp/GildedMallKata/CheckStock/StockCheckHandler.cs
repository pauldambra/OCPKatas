using System.Collections.Generic;
using System.Linq;
using GildedMallKata.Stock;
using GildMallKata;

namespace GildedMallKata.CheckStock
{
    public class StockCheckHandler : IHandle<CheckStockAsAtDate, IEnumerable<StockItem>>
    {
        private readonly GildedStockManager _shop;

        public StockCheckHandler(GildedStockManager shop)
        {
            _shop = shop;
        }

        public IEnumerable<StockItem> Handle(CheckStockAsAtDate checkStock)
        {
            return _shop.StockList.OfType<Dress>()
                .Select(dsi => dsi.AsAtDate(checkStock.StockCheckDate));
        }
    }
}