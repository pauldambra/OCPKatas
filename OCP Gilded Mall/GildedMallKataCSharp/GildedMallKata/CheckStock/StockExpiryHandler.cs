using System.Collections.Generic;
using System.Linq;
using GildedMallKata.Stock;
using GildMallKata;

namespace GildedMallKata.CheckStock
{
    public class StockExpiryHandler : IHandle<CheckStockAsAtDate, IEnumerable<TinnedFood>>
    {
        private readonly GildedStockManager _shop;

        public StockExpiryHandler(GildedStockManager shop)
        {
            _shop = shop;
        }

        public IEnumerable<TinnedFood> Handle(CheckStockAsAtDate checkStock)
        {
            return _shop.StockList
                .OfType<TinnedFood>()
                .Where(dsi => dsi.IsSaleable(checkStock.StockCheckDate));
        }
    }
}